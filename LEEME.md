# Tech Solutions – Sistema de clientes, productos y ventas (con seguridad)

C# · Windows Forms · .NET Framework 4.8 · SQL Server · 4 capas.

## Cómo ejecutarlo
1. En SSMS abra `00_BD_Completa.sql` y ejecútelo (F5). Crea la base **TechSolutions** con clientes y productos de ejemplo. **No inserta usuarios.**
2. Abra `Solucion_TechSolutions/TechSolutions.sln` en Visual Studio.
3. En `Presentacion/App.config` ajuste `Data Source` a su servidor (`.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`, etc.).
4. `Presentacion` debe ser el proyecto de inicio. Ejecute (F5).
5. La primera vez la aplicación pide **crear al Administrador** (no está en la BD). Después aparece el login.

## Arquitectura (4 capas)
| Capa | Contenido | Referencia a |
|---|---|---|
| **Entidades** | Usuario, Cliente, Producto, Venta, VentaReporte, Roles, NegocioException | nada |
| **Datos** | `Conexion` (**Singleton**), `UsuarioDAL`, `ClienteDAL`, `ProductoDAL`, `VentaDAL` | Entidades |
| **Negocio** | `Seguridad` (sal+hash), `Sesion` (**Singleton**), `Permisos`, `UsuarioBLL`, `ClienteBLL`, `ProductoBLL`, `VentaBLL` | Entidades, Datos |
| **Presentacion** | Formularios (login, menú, usuarios, clientes, productos, ventas, reporte) | Entidades, Negocio (**no** Datos) |

## Roles y menú
| Módulo | Administrador | Vendedor | Almacenero |
|---|:-:|:-:|:-:|
| Clientes | sí | sí | no |
| Productos / inventario | sí | no | sí |
| Ventas | sí | sí | no |
| Reportes (reporte de clientes, visor .rdlc) y Consulta de ventas | sí | no | no |
| Usuarios (crear / desbloquear) | sí | no | no |

La tabla vive en un solo lugar: `Negocio/Permisos.cs`. El menú la usa para **mostrar u ocultar** opciones, y cada método de la capa de Negocio la usa para **negar** la operación aunque alguien llegue a ella sin pasar por el menú.

## Medidas de seguridad implementadas
- **Contraseñas con sal + hash:** PBKDF2-SHA256, 600 000 iteraciones, sal aleatoria de 16 bytes por usuario (generador criptográfico). La contraseña **nunca** se guarda; la BD solo tiene `ClaveHash` y `Sal`.
- **Comparación de hash en tiempo constante** y **tiempo equalizado** cuando el usuario no existe (evita descubrir usuarios midiendo la demora).
- **Bloqueo a los 3 intentos fallidos:** el contador se incrementa y la cuenta se bloquea en **una sola sentencia SQL atómica**. Una cuenta bloqueada no prueba la contraseña. Solo un Administrador puede desbloquearla (módulo Usuarios). Un login correcto reinicia el contador.
- **Mensaje de login genérico:** "Usuario o contraseña incorrectos" (no dice cuál falló).
- **Primer administrador fuera de la BD:** lo crea la aplicación en el primer arranque, con inserción atómica (no pueden crearse dos "primeros" a la vez).
- **Inyección SQL:** todas las consultas son parametrizadas, con tipo y tamaño iguales a la columna. La búsqueda con `LIKE` escapa los comodines. No existe ninguna consulta concatenada.
- **Autorización en el servidor lógico (Negocio)**, no solo en el menú.
- **Singleton:** `Conexion` y `Sesion`, ambos con `Lazy<T>` (seguros con varios hilos). La conexión no se comparte: cada operación abre y cierra la suya.
- **Transacción de venta:** descuenta stock y registra la venta juntos. `UPDATE ... WHERE Stock >= @cant` impide el stock negativo incluso con ventas simultáneas; la BD además tiene `CHECK (Stock >= 0)`.
- **El precio y el vendedor no vienen de la pantalla:** el precio lo toma la BD al vender y el vendedor sale de la sesión.
- **Sin filtrar información interna:** los errores inesperados no muestran `ex.Message`; van a `Trace`. La grilla de usuarios no consulta ni muestra hash ni sal.
- **Cadena de conexión con autenticación de Windows:** no hay usuario ni contraseña de BD en `App.config`.
- Política de contraseña: 8–64 caracteres, con mayúscula, minúscula y número.

## Funciones agregadas en la segunda etapa
- **Transacciones COMMIT / ROLLBACK** (`Datos/VentaDAL.cs`):
  - *Registrar venta*: descuenta stock + inserta la venta. Si algo falla → `ROLLBACK`; si todo sale bien → `COMMIT`.
  - *Anular venta* (nuevo, solo Administrador, en **Reportes**): marca la venta como anulada + devuelve el stock, también en una transacción. Una venta no puede anularse dos veces (ni con dos usuarios a la vez).
- **Timer de cierre de sesión por inactividad:**
  - `FrmPrincipal` tiene un `Timer` (1 s) que muestra la cuenta regresiva y, al vencer, cierra las ventanas abiertas y vuelve al login con un aviso.
  - `FiltroInactividad` detecta mouse y teclado en toda la aplicación (incluidas las ventanas hijas) y reinicia el contador.
  - **Además se aplica en Negocio** (`Sesion` + `Permisos.Exigir`): una sesión vencida no puede ejecutar ninguna operación aunque la pantalla siga abierta.
  - Tiempo configurable en `App.config` → `MinutosInactividad` (por defecto 5; use 1 para probar).
- **Consultas con LINQ** (en Negocio):
  - `VentaBLL.Resumir`: `Where` + `GroupBy` + `Select` + `OrderByDescending` → ventas por **producto, vendedor o cliente** (combo "Vista" en Reportes).
  - `VentaBLL.TotalVigente`: `Where` + `Sum` (las ventas anuladas no suman).
  - `ProductoBLL.StockBajo`: `Where` + `OrderBy` + `ThenBy` → casilla "Solo stock bajo" en Productos.

**Actualizar su BD existente:** vuelva a ejecutar `00_BD_Completa.sql` (no borra datos); agrega las columnas de anulación a `Venta`.

## Reporte de clientes (procedimiento almacenado) e instalador
**Reporte de clientes** (menú *Reportes*, o Clientes → botón *Reporte de clientes*; este botón lo ven Administrador y Vendedor):
| Capa | Archivo | Qué hace |
|---|---|---|
| SQL Server | `01_BD_ReporteClientes.sql` | Procedimiento `dbo.usp_ReporteClientes (@Nombre)`; `''` = todos. Los comodines `% _ [` se buscan como texto. |
| Datos | `Datos/ReporteClienteDAL.cs` | `SqlCommand` con `CommandType.StoredProcedure` y parámetro `@Nombre` → `List<Cliente>` |
| Negocio | `Negocio/ReporteClienteBLL.cs` | Exige el permiso de Clientes, recorta el nombre y limita a 100 caracteres |
| Presentación | `FrmReporteClientes` + `ReporteClientes.rdlc` | Visor `ReportViewer` con `DataSet1` y el parámetro `Filtro`. Desde el visor se imprime o se exporta a PDF. |

**Antes de compilar (una vez, con Internet):** instale en Visual Studio las extensiones *Microsoft RDLC Report Designer* y *Microsoft Visual Studio Installer Projects 2022* (Extensiones → Administrar extensiones). Al compilar se descarga solo el paquete NuGet del visor.
**Base de datos:** ejecute `00_BD_Completa.sql` y luego `01_BD_ReporteClientes.sql`.

**Instalador** (`Solucion_TechSolutions/Instalador/Instalador.vdproj`, ya configurado):
- Producto «Sistema de Ventas», fabricante «Tech Solutions», versión 1.0.0, `RemovePreviousVersions = True`.
- Instala en `C:\Program Files (x86)\Tech Solutions\Sistema de Ventas`: el programa (`TechSolutions.exe` + `.config` + las DLL de las capas y del visor), `tech.ico`, los dos `.sql` y `LEEME_Instalacion.txt`.
- Accesos directos «Tech Solutions» con ícono en el Escritorio y en el menú Inicio.
- Requisito previo .NET Framework 4.8 (genera `setup.exe`).

Para generarlo: barra **Debug → Release** → *Compilar solución* → clic derecho en **Instalador → Compilar** (o *Recompilar* si cambió algo). Quedan en `Instalador\Release`: **`setup.exe`** + **`Instalador.msi`** (se entregan juntos; se ejecuta `setup.exe`).
Nueva versión: suba `AssemblyVersion` en `Presentacion/Properties/AssemblyInfo.cs` (p. ej. 1.1.0.0) **y** la `Version` del Instalador (F4 → 1.1.0; ahí Visual Studio pregunta si cambia el ProductCode: responda **Sí**).

## Guía de pruebas
1. **Primer arranque:** se pide crear el administrador. Verifique en SSMS que `Usuario.ClaveHash` y `Usuario.Sal` son texto ilegible y que no aparece la contraseña.
2. **Bloqueo:** cree un usuario `vendedor1`, cierre sesión y falle la contraseña 3 veces → "Cuenta bloqueada". Con la contraseña correcta sigue bloqueada. Ingrese como admin → Usuarios → *Desbloquear seleccionado*.
3. **Roles:** ingrese con cada rol y compare el menú con la tabla. Un Almacenero no ve Clientes, Ventas, Reportes ni Usuarios.
4. **Inyección SQL:** en Clientes → Buscar escriba `' OR 1=1 --` → devuelve 0 filas (se busca como texto). En el login, usuario `admin' --` → "Usuario o contraseña incorrectos".
6. **Inactividad:** ponga `MinutosInactividad` en `1`, ingrese y no toque nada: la cuenta regresiva del menú llega a 00:00, se cierran las ventanas y aparece el login con el aviso. Mover el mouse o teclear reinicia la cuenta.
7. **Anulación (COMMIT):** como admin, **Consulta de ventas** → vista Detalle → seleccione una venta → *Anular*. El estado pasa a ANULADA, el total vigente baja y el stock del producto sube. Intente anularla otra vez → "ya fue anulada".
8. **LINQ:** en **Consulta de ventas** cambie la vista a *Por producto / vendedor / cliente*; en Productos marque *Solo stock bajo*.
9. **Reporte de clientes:** menú *Reportes* (el reporte aparece solo, sin escribir nada): los 3 clientes de ejemplo, «Filtro: Todos», «Total de clientes: 3». Escriba `ana` → solo Ana Torres Ríos. Escriba `xyz` → tabla vacía, total 0. Escriba `%` → 0 filas. Con el rol *Almacenero* no se ve el módulo Clientes. *Exportar → PDF* guarda el PDF.
10. **Instalador:** compile el Instalador, ejecute `setup.exe` en otra PC (con los dos scripts SQL ya ejecutados), abra «Tech Solutions» desde el Escritorio (con ícono) y verifique que aparece en Configuración → Aplicaciones → Sistema de Ventas.
11. **Stock:** venda más unidades que el stock → "Stock insuficiente" y no cambia nada. Venda correctamente → el stock baja y el reporte lo muestra.

## Si el único administrador queda bloqueado
Nadie puede desbloquearlo desde la aplicación. Un DBA lo resuelve con el `UPDATE` comentado al final de `00_BD_Completa.sql`.

## Decisiones de diseño a tener en cuenta
- El bloqueo por usuario permite que un tercero bloquee una cuenta ajena fallando a propósito; es el compromiso habitual de este mecanismo (la protección es el desbloqueo por un administrador).
- Una venta tiene un solo producto (sencillo y transaccional). Si luego necesita varios productos por venta, se agrega una tabla `DetalleVenta` sin cambiar las capas.
