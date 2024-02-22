-- Base de datos creada por Giovanni Najera para la prueba tecnica del Banco Atlantida

CREATE DATABASE GestorCuentasTarjetasDB;
GO


USE GestorCuentasTarjetasDB;
GO

-- Crear la tabla de TarjetasCredito
CREATE TABLE TarjetasCredito (
    IdTarjeta INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TitularNombre VARCHAR(100) NOT NULL,
    NumeroTarjeta VARCHAR(16) NOT NULL,
    SaldoActual DECIMAL(10, 2) NOT NULL,
    LimiteCredito DECIMAL(10, 2) NOT NULL,
    SaldoDisponible DECIMAL(10, 2) NOT NULL
);

-- Crear la tabla de Compras
CREATE TABLE Compras (
    CompraId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TarjetaCreditoId INT NULL,
    FechaCompra DATE NOT NULL,
    Descripcion VARCHAR(255) NOT NULL,
    Monto NUMERIC(10, 2) NOT NULL
    FOREIGN KEY (TarjetaCreditoId) REFERENCES TarjetasCredito(IdTarjeta)
);

DROP TABLE Pagos;

-- Crear la tabla de Pagos
CREATE TABLE Pagos (
    PagoId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TarjetaCreditoId INT NULL,
    FechaPago DATE NOT NULL,
    Monto NUMERIC(10, 2) NOT NULL,
    FOREIGN KEY (TarjetaCreditoId) REFERENCES TarjetasCredito(IdTarjeta)
);
GO

---PROEDIMIENTOS ALMACENADOS

-- Procedimiento almacenado para obtener todas las transacciones de una tarjeta de crédito por mes
CREATE PROCEDURE Sp_ObtenerTransaccionesPorMes
    @TarjetaCreditoId INT,
    @Mes INT,
    @Anio INT
AS
BEGIN
    DECLARE @FechaInicioMes DATE = DATEFROMPARTS(@Anio, @Mes, 1);
    DECLARE @FechaFinMes DATE = EOMONTH(@FechaInicioMes);

    SELECT 
        'Compra' AS TipoTransaccion, 
        FechaCompra AS Fecha, 
        Descripcion, 
        Monto, 
        (SELECT SUM(Monto) FROM Compras WHERE TarjetaCreditoId = @TarjetaCreditoId AND FechaCompra BETWEEN @FechaInicioMes AND @FechaFinMes) AS TotalCompras
    FROM Compras
    WHERE TarjetaCreditoId = @TarjetaCreditoId
      AND FechaCompra BETWEEN @FechaInicioMes AND @FechaFinMes

    UNION

    SELECT 'Pago' AS TipoTransaccion, FechaPago AS Fecha, 'Pago' AS Descripcion, Monto, NULL AS TotalCompras
    FROM Pagos
    WHERE TarjetaCreditoId = @TarjetaCreditoId
      AND FechaPago BETWEEN @FechaInicioMes AND @FechaFinMes

    ORDER BY Fecha DESC;
END;
GO

-- Procedimiento almacenado para Agregar una Nueva Compra
CREATE PROCEDURE Sp_AgregarCompra
    @TarjetaCreditoId INT,
    @FechaCompra DATE,
    @Descripcion VARCHAR(255),
    @Monto DECIMAL(10, 2)
AS
BEGIN
    -- Insertar la nueva compra en la tabla Compras
    INSERT INTO Compras (TarjetaCreditoId, FechaCompra, Descripcion, Monto)
    VALUES (@TarjetaCreditoId, @FechaCompra, @Descripcion, @Monto);
END;
GO

-- Procedimiento almacenado para Agregar un Nuevo Pago
CREATE PROCEDURE Sp_AgregarPago
    @TarjetaCreditoId INT,
    @FechaPago DATE,
    @Monto DECIMAL(10, 2)
AS
BEGIN
    -- Insertar el nuevo pago en la tabla Pagos
    INSERT INTO Pagos (TarjetaCreditoId, FechaPago, Monto)
    VALUES (@TarjetaCreditoId, @FechaPago, @Monto);
END;
GO

CREATE PROCEDURE Sp_ObtenerComprasPorMes
    @TarjetaCreditoId INT,
    @Mes INT,
    @Anio INT
AS
BEGIN
    DECLARE @FechaInicioMes DATE = DATEFROMPARTS(@Anio, @Mes, 1);
    DECLARE @FechaFinMes DATE = EOMONTH(@FechaInicioMes);

    SELECT 
        CompraId, 
        TarjetaCreditoId,
        FechaCompra, 
        Descripcion, 
        Monto,
        (SELECT SUM(Monto) FROM Compras WHERE TarjetaCreditoId = @TarjetaCreditoId AND FechaCompra BETWEEN @FechaInicioMes AND @FechaFinMes) AS TotalComprasMes
    FROM Compras
    WHERE TarjetaCreditoId = @TarjetaCreditoId
      AND FechaCompra BETWEEN @FechaInicioMes AND @FechaFinMes
    ORDER BY FechaCompra DESC;
END;
GO

-- Procedimiento almacenado para obtener el estado de cuenta de una tarjeta por Id

CREATE PROCEDURE Sp_ObtenerTransaccionesPorTarjetaId
    @TarjetaCreditoId INT
AS
BEGIN
    SELECT 
        IdTarjeta,
        TitularNombre,
        NumeroTarjeta,
        SaldoActual,
        LimiteCredito,
        SaldoDisponible
    FROM TarjetasCredito
    WHERE IdTarjeta = @TarjetaCreditoId;

    -- Obtener todas las transacciones (compras y pagos) para la tarjeta proporcionada
    SELECT 
        'Compra' AS TipoTransaccion, 
        FechaCompra AS Fecha, 
        Descripcion, 
        Monto,
        NULL AS TotalCompras
    FROM Compras
    WHERE TarjetaCreditoId = @TarjetaCreditoId

    UNION

    SELECT 
        'Pago' AS TipoTransaccion, 
        FechaPago AS Fecha, 
        'Pago' AS Descripcion, 
        Monto,
        NULL AS TotalCompras
    FROM Pagos
    WHERE TarjetaCreditoId = @TarjetaCreditoId

    UNION

    SELECT 
        NULL AS TipoTransaccion,
        NULL AS Fecha,
        NULL AS Descripcion,
        NULL AS Monto,
        SUM(Monto) AS TotalCompras
    FROM Compras
    WHERE TarjetaCreditoId = @TarjetaCreditoId
    
    ORDER BY Fecha DESC;
END;
GO

-- Procedimiento almacenado para bucar tarjeta de Credito por Nombre
CREATE PROCEDURE Sp_BuscarTarjetasPorNombre
    @NombreTitular VARCHAR(100)
AS
BEGIN
    SELECT *
    FROM TarjetasCredito
    WHERE TitularNombre LIKE '%' + @NombreTitular + '%';
END;
GO



