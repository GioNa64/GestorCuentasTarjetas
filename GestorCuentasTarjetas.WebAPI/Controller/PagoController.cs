using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using GestorCuentasTarjetas.WebAPI.Models;
using Microsoft.Data.SqlClient;


namespace GestorCuentasTarjetas.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        public readonly DbContexto _dbContexto;

        public PagoController(DbContexto _context)
        {
            _dbContexto = _context;
        }


        [HttpPost]
        [Route("AgregarPago")]
        public IActionResult AgregarPago([FromBody] Pago nuevoPago)
        {
            try
            {
                var pago = new Pago
                {
                    TarjetaCreditoId = nuevoPago.TarjetaCreditoId,
                    Monto = nuevoPago.Monto,
                    FechaPago = DateTime.Now
                };

                _dbContexto.Pagos.Add(pago);
                _dbContexto.SaveChanges();

                return StatusCode(201, new { mensaje = "Pago creado exitosamente", pago });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al crear el pago: {ex.Message}" });
            }
        }


    }
}
