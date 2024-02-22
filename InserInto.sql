--Insertar Datos Iniciales para GestorCuentasDB


-- Insertar datos en la tabla TarjetasCredito
INSERT INTO TarjetasCredito (TitularNombre, NumeroTarjeta, SaldoActual, LimiteCredito, SaldoDisponible)
VALUES
    ('Juan Perez', '1234567812345678', 500.00, 1000.00, 500.00),
    ('Carlos Hernandez', '9876543210987654', 800.00, 1500.00, 800.00),
    ('Maria Lopez', '1111222233334444', 1200.00, 2000.00, 1200.00);

-- Insertar datos en la tabla Compras
INSERT INTO Compras (TarjetaCreditoId, FechaCompra, Descripcion, Monto)
VALUES
    (1, '2024-02-1', 'Compra de productos electrónicos', 200.00),
    (1, '2024-02-15', 'Compra de ropa', 150.00),
    (2, '2024-02-14', 'Compra de comestibles', 50.00);


-- Insertar datos en la tabla Pagos
INSERT INTO Pagos (TarjetaCreditoId, FechaPago, Monto)
VALUES
    (1, '2024-02-29', 100.00),
    (1, '2024-02-18', 50.00),
    (3, '2024-02-16', 80.00);
