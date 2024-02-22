using Microsoft.AspNetCore.Mvc;
using GestorCuentasTarjetas.FrontendMvc.Models;

namespace GestorCuentasTarjetas.FrontendMvc.Controllers
{
    public class TransaccionController : Controller
    {
      
        [HttpGet]
        [Route("ObtenerTranssacion/{idTarjeta}")]
        public async Task<IActionResult> ObtenerTranssacion(int idTarjeta)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:7165/api/Tarjeta/ObtenerEstadoCuentaDetallado/{idTarjeta}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        EstadoCuenta estadoCuentaResponse = await response.Content.ReadAsAsync<EstadoCuenta>();
                        Tarjeta ptarjeta = new Tarjeta();

                        var PorcentajeInteresConfigurable = 25;

                        var InteresBonificable = ptarjeta.SaldoActual * PorcentajeInteresConfigurable;

                        ViewBag.InteresBonificable = InteresBonificable;


                        var transacciones = estadoCuentaResponse.Transacciones;


                        return View(estadoCuentaResponse);
                    }
                    else
                    {
                        return View(new EstadoCuenta());
                    }
                }
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, $"Error : {ex.Message}");

            }
        }
    }
}
