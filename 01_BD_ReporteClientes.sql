-- REPORTE DE CLIENTES · Procedimiento almacenado (SQL Server, SSMS -> F5)
-- Ejecutar DESPUÉS de 00_BD_Completa.sql. Se puede volver a ejecutar sin errores (no borra datos).
USE TechSolutions;
GO

-- Devuelve los clientes cuyo nombre contiene @Nombre ('' = todos), ordenados por nombre.
-- Los comodines de LIKE (% _ [) que escriba el usuario se "escapan": se buscan como texto normal.
CREATE OR ALTER PROCEDURE dbo.usp_ReporteClientes
    @Nombre VARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @patron VARCHAR(220) =
        '%' + REPLACE(REPLACE(REPLACE(REPLACE(@Nombre, '\', '\\'), '%', '\%'), '_', '\_'), '[', '\[') + '%';

    SELECT Documento,
           Nombre,
           ISNULL(Correo, '')   AS Correo,
           ISNULL(Telefono, '') AS Telefono
    FROM dbo.Cliente
    WHERE Nombre LIKE @patron ESCAPE '\'
    ORDER BY Nombre;
END
GO

-- Pruebas
EXEC dbo.usp_ReporteClientes;                         -- todos (3 si solo están los clientes de ejemplo)
EXEC dbo.usp_ReporteClientes @Nombre = 'ana';         -- Ana Torres Ríos
EXEC dbo.usp_ReporteClientes @Nombre = '%';           -- 0 filas: el % se busca como texto, no como comodín
GO
