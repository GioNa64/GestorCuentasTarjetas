using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using GestorCuentasTarjetas.WebAPI.Models;
using Microsoft.Data.SqlClient;

namespace GestorCuentasTarjetas.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        public readonly DbContexto _dbContexto;

        public CompraController(DbContexto _context)
        {
            _dbContexto = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Compra> lista = new List<Compra>();

            try
            {
                lista = _dbContexto.Compras.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("ComprasPorMes/{tarjetaCreditoId}/{mes}/{anio}")]
        public IActionResult ObtenerComprasPorMes(int tarjetaCreditoId, int mes, int anio)
        {
            try
            {
                var parametros = new[]
                {
            new SqlParameter("@TarjetaCreditoId", tarjetaCreditoId),
            new SqlParameter("@Mes", mes),
            new SqlParameter("@Anio", anio)
        };

                var comprasPorMes = _dbContexto.Compras.FromSqlRaw("Sp_ObtenerComprasPorMes @TarjetaCreditoId, @Mes, @Anio", parametros).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = comprasPorMes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet("transaccionesPorMes/{tarjetaCreditoId}/{mes}/{anio}")]
        public IActionResult ObtenerTransaccionesPorMes(int tarjetaCreditoId, int mes, int anio)
        {
            try
            {
                var parametros = new[]
                {
            new SqlParameter("@TarjetaCreditoId", tarjetaCreditoId),
            new SqlParameter("@Mes", mes),
            new SqlParameter("@Anio", anio),
        };

                var transacciones = _dbContexto.Set<Transaccion>()
                    .FromSqlRaw("EXEC Sp_ObtenerTransaccionesPorMes @TarjetaCreditoId, @Mes, @Anio", parametros)
                    .ToList();

                return Ok(transacciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las transacciones: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("CrearCompra")]
        public IActionResult CrearCompra([FromBody] Compra nuevaCompra)
        {
            try
            {
                var compra = new Compra
                {
                    TarjetaCreditoId = nuevaCompra.TarjetaCreditoId,
                    Monto = nuevaCompra.Monto,
                    Descripcion = nuevaCompra.Descripcion,
                    FechaCompra = DateTime.Now 
                };

                _dbContexto.Compras.Add(compra);
                _dbContexto.SaveChanges();
                return StatusCode(201, new { mensaje = "Compra creada exitosamente", compra });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al crear la compra: {ex.Message}" });
            }
        }







    }
}
