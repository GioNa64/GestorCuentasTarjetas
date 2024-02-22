# Gestor de Tarjetas de Credito

Este proyecto es una aplicación para gestionar cuentas y tarjetas bancarias. Proporciona funcionalidades como el manejo de tarjetas de crédito, compras, pagos, y la generación de informes de transacciones.

## Estructura de la Base de Datos

El sistema utiliza una base de datos SQL Server llamada `GestorCuentasTarjetasDB`. A continuación, se describe la estructura de las tablas y algunos procedimientos almacenados clave.

### Tablas

1. **TarjetasCredito:** Almacena la información de las tarjetas de crédito, como el titular, número de tarjeta, saldo actual, límite de crédito y saldo disponible.

2. **Compras:** Registra las compras realizadas con las tarjetas de crédito, incluyendo la fecha, descripción y monto.

3. **Pagos:** Almacena los pagos realizados en las tarjetas de crédito, con información sobre la fecha y monto.

### Procedimientos Almacenados

1. **Sp_ObtenerTransaccionesPorMes:** Obtiene todas las transacciones de una tarjeta de crédito para un mes y año específicos.

2. **Sp_AgregarCompra:** Agrega una nueva compra a la base de datos.

3. **Sp_AgregarPago:** Agrega un nuevo pago a la base de datos.

4. **Sp_ObtenerComprasPorMes:** Obtiene las compras realizadas en un mes y año específicos para una tarjeta de crédito.

5. **Sp_ObtenerTransaccionesPorTarjetaId:** Obtiene el estado de cuenta de una tarjeta de crédito, incluyendo todas las transacciones.

6. **Sp_BuscarTarjetasPorNombre:** Busca tarjetas de crédito por el nombre del titular.

## Uso

Para poder usar debe ejecutar el script llamado GestorCuentasTarjetasDB, para tener datos en la tablas ejecute el script llamado InserInto y para probar los Procedimientoss almacenados ejecute el script EjecutarSp de uno en uno.



