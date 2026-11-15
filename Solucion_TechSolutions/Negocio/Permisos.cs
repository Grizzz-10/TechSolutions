// CAPA NEGOCIO – qué rol puede usar cada módulo.
// UNA sola tabla de permisos, usada por dos lados:
//   - la pantalla, para MOSTRAR u ocultar el menú (comodidad);
//   - esta capa, para NEGAR la operación (seguridad real: ocultar un botón no protege nada).
using System;
using System.Collections.Generic;
using Entidades;

namespace Negocio
{
    public enum Modulo { Clientes, Productos, Ventas, Reportes, Usuarios }

    public static class Permisos
    {
        //                         Administrador   Vendedor   Almacenero
        // Clientes                     sí            sí          no
        // Productos (inventario)       sí            no          sí
        // Ventas                       sí            sí          no
        // Reportes de ventas           sí            no          no
        // Usuarios                     sí            no          no
        private static readonly Dictionary<Modulo, string[]> tabla = new Dictionary<Modulo, string[]>
        {
            { Modulo.Clientes,  new[] { Roles.Administrador, Roles.Vendedor } },
            { Modulo.Productos, new[] { Roles.Administrador, Roles.Almacenero } },
            { Modulo.Ventas,    new[] { Roles.Administrador, Roles.Vendedor } },
            { Modulo.Reportes,  new[] { Roles.Administrador } },
            { Modulo.Usuarios,  new[] { Roles.Administrador } }
        };

        public static bool Puede(string rol, Modulo modulo)
        {
            return rol != null && Array.IndexOf(tabla[modulo], rol) >= 0;
        }

        // Verifica la SESIÓN ACTUAL. Si no tiene permiso en ninguno de los módulos indicados, lanza excepción.
        public static void Exigir(params Modulo[] modulos)
        {
            Sesion sesion = Sesion.Instancia;

            // Inactividad: aunque la pantalla no lo haya detectado, una sesión vencida NO puede operar.
            if (sesion.Expirada)
            {
                sesion.CerrarPorInactividad();
                throw new UnauthorizedAccessException("Su sesión expiró por inactividad. Inicie sesión de nuevo.");
            }

            Usuario u = sesion.Usuario;
            if (u != null)
                foreach (Modulo m in modulos)
                    if (Puede(u.Rol, m))
                    {
                        sesion.RegistrarActividad();     // hacer una operación cuenta como actividad
                        return;
                    }
            throw new UnauthorizedAccessException("No tiene permiso para realizar esta operación.");
        }
    }
}
