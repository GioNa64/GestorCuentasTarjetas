
--Comandos para ejecutar Procedimientos Almacenados

----OBETENER LISTA DE COMPRAS Y PAGOS

DECLARE @TarjetaCreditoIdTest INT;
DECLARE @MesTest INT;
DECLARE @AnioTest INT;

SET @TarjetaCreditoIdTest = 1; 
SET @MesTest = 2;
SET @AnioTest = 2024;

EXEC Sp_ObtenerTransaccionesPorMes @TarjetaCreditoIdTest, @MesTest, @AnioTest;


----------AGREGAR UNA NUEVA COMPRA

DECLARE @TarjetaCreditoIdTest INT;
DECLARE @FechaCompraTest DATE;
DECLARE @DescripcionTest VARCHAR(255);
DECLARE @MontoTest DECIMAL(10, 2);

SET @TarjetaCreditoIdTest = 1;
SET @FechaCompraTest = '2024-01-2';
SET @DescripcionTest = 'Compra de Zapatos';
SET @MontoTest = 50.00;


EXEC Sp_AgregarCompra @TarjetaCreditoIdTest, @FechaCompraTest, @DescripcionTest, @MontoTest;


----------AGREGAR UN NUEVO PAGO

DECLARE @TarjetaCreditoIdTest INT;
DECLARE @FechaPagoTest DATE;
DECLARE @MontoPagoTest DECIMAL(10, 2);

SET @TarjetaCreditoIdTest = 2;
SET @FechaPagoTest = '2024-02-16';
SET @MontoPagoTest = 55.00; 


EXEC Sp_AgregarPago @TarjetaCreditoIdTest, @FechaPagoTest, @MontoPagoTest;

----------OBTENER COMPRAS Y TOTAL 

DECLARE @TarjetaCreditoIdTest INT;
DECLARE @MesTest INT;
DECLARE @AnioTest INT;

SET @TarjetaCreditoIdTest = 2; 
SET @MesTest = 2;
SET @AnioTest = 2024; 

EXEC Sp_ObtenerComprasPorMes @TarjetaCreditoIdTest, @MesTest, @AnioTest;


---------OBTENER ESTADO DE CUENTA DE UNA TARJETA
DECLARE @TarjetaCreditoId INT = 1;  

-- Ejecuta el procedimiento almacenado
EXEC Sp_ObtenerTransaccionesPorTarjetaId @TarjetaCreditoId;



