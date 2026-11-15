
IF DB_ID('TechSolutions') IS NULL CREATE DATABASE TechSolutions;
GO
USE TechSolutions;
GO

-- ============================== CLIENTES ==============================
IF OBJECT_ID('dbo.Cliente') IS NULL
CREATE TABLE dbo.Cliente (
    IdCliente  INT IDENTITY(1,1) PRIMARY KEY,
    Documento  VARCHAR(11)  NOT NULL UNIQUE,      -- DNI (8) o RUC (11)
    Nombre     VARCHAR(100) NOT NULL,
    Correo     VARCHAR(100) NULL,
    Telefono   VARCHAR(15)  NULL
);
GO

-- ============================== PRODUCTOS ==============================
IF OBJECT_ID('dbo.Producto') IS NULL
CREATE TABLE dbo.Producto (
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    Codigo     VARCHAR(20)   NOT NULL UNIQUE,
    Nombre     VARCHAR(100)  NOT NULL,
    Precio     DECIMAL(10,2) NOT NULL CHECK (Precio > 0),
    Stock      INT           NOT NULL DEFAULT 0 CHECK (Stock >= 0)
);
GO

-- ============================== USUARIOS ==============================
IF OBJECT_ID('dbo.Usuario') IS NULL
CREATE TABLE dbo.Usuario (
    IdUsuario        INT IDENTITY(1,1) PRIMARY KEY,
    Login            VARCHAR(30)  NOT NULL UNIQUE,
    ClaveHash        VARCHAR(100) NOT NULL,
    Sal              VARCHAR(50)  NOT NULL,
    Rol              VARCHAR(20)  NOT NULL
                     CHECK (Rol IN ('Administrador','Vendedor','Almacenero')),
    IntentosFallidos INT          NOT NULL DEFAULT 0,
    Bloqueado        BIT          NOT NULL DEFAULT 0
);
GO

-- ============================== VENTAS ==============================
IF OBJECT_ID('dbo.Venta') IS NULL
CREATE TABLE dbo.Venta (
    IdVenta        INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente      INT           NOT NULL REFERENCES dbo.Cliente(IdCliente),
    IdProducto     INT           NOT NULL REFERENCES dbo.Producto(IdProducto),
    Cantidad       INT           NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(10,2) NOT NULL CHECK (PrecioUnitario > 0),
    Total          AS (Cantidad * PrecioUnitario) PERSISTED,
    Fecha          DATETIME2(0)  NOT NULL DEFAULT SYSDATETIME(),
    IdUsuario      INT           NOT NULL REFERENCES dbo.Usuario(IdUsuario)
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Venta_Fecha' AND object_id = OBJECT_ID('dbo.Venta'))
    CREATE INDEX IX_Venta_Fecha ON dbo.Venta (Fecha);
GO

-- ============================== ANULACIÓN DE VENTAS (se puede re-ejecutar; actualiza una BD ya creada) ==============================
IF COL_LENGTH('dbo.Venta', 'Anulada') IS NULL
    ALTER TABLE dbo.Venta ADD Anulada BIT NOT NULL CONSTRAINT DF_Venta_Anulada DEFAULT 0;
IF COL_LENGTH('dbo.Venta', 'FechaAnulacion') IS NULL
    ALTER TABLE dbo.Venta ADD FechaAnulacion DATETIME2(0) NULL;
IF COL_LENGTH('dbo.Venta', 'IdUsuarioAnula') IS NULL
    ALTER TABLE dbo.Venta ADD IdUsuarioAnula INT NULL CONSTRAINT FK_Venta_UsuarioAnula REFERENCES dbo.Usuario(IdUsuario);
GO

-- ============================== DATOS DE EJEMPLO (clientes y productos; NO usuarios) ==============================
IF NOT EXISTS (SELECT 1 FROM dbo.Cliente WHERE Documento = '45781236')
    INSERT INTO dbo.Cliente (Documento, Nombre, Correo, Telefono) VALUES ('45781236', 'Ana Torres Ríos', 'ana.torres@correo.com', '987654321');
IF NOT EXISTS (SELECT 1 FROM dbo.Cliente WHERE Documento = '70125489')
    INSERT INTO dbo.Cliente (Documento, Nombre, Correo, Telefono) VALUES ('70125489', 'Luis Quispe Mamani', NULL, '912345678');
IF NOT EXISTS (SELECT 1 FROM dbo.Cliente WHERE Documento = '20512345678')
    INSERT INTO dbo.Cliente (Documento, Nombre, Correo, Telefono) VALUES ('20512345678', 'Comercial Andina S.A.C.', 'compras@andina.pe', '014567890');

IF NOT EXISTS (SELECT 1 FROM dbo.Producto WHERE Codigo = 'LAP-001')
    INSERT INTO dbo.Producto (Codigo, Nombre, Precio, Stock) VALUES ('LAP-001', 'Laptop 15" Core i5 16GB', 2899.90, 12);
IF NOT EXISTS (SELECT 1 FROM dbo.Producto WHERE Codigo = 'MON-024')
    INSERT INTO dbo.Producto (Codigo, Nombre, Precio, Stock) VALUES ('MON-024', 'Monitor 24" Full HD', 549.00, 25);
IF NOT EXISTS (SELECT 1 FROM dbo.Producto WHERE Codigo = 'TEC-MEC')
    INSERT INTO dbo.Producto (Codigo, Nombre, Precio, Stock) VALUES ('TEC-MEC', 'Teclado mecánico', 189.50, 40);
IF NOT EXISTS (SELECT 1 FROM dbo.Producto WHERE Codigo = 'MOU-INA')
    INSERT INTO dbo.Producto (Codigo, Nombre, Precio, Stock) VALUES ('MOU-INA', 'Mouse inalámbrico', 69.90, 60);
GO

SELECT * FROM dbo.Cliente;
SELECT * FROM dbo.Producto;
SELECT IdUsuario, Login, Rol, IntentosFallidos, Bloqueado FROM dbo.Usuario;
GO

-- ====================================================================================================
-- SOLO EMERGENCIAS (comentado a propósito): si el ÚNICO administrador quedó bloqueado, nadie puede
-- desbloquearlo desde la aplicación. Un DBA puede hacerlo así (cambie 'admin' por su usuario):
--
--   UPDATE dbo.Usuario SET IntentosFallidos = 0, Bloqueado = 0 WHERE Login = 'admin';
-- ====================================================================================================
