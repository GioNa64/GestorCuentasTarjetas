using Microsoft.AspNetCore.Mvc;

using GestorCuentasTarjetas.FrontendMvc.Models;

namespace GestorCuentasTarjetas.FrontendMvc.Controllers
{
    public class PagoController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public PagoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        [Route("CrearPago/{idTarjeta}")]
        public IActionResult CrearPago(int idTarjeta)
        {
            return View();
        }



        [HttpPost]
        [Route("CrearPago/{idTarjeta}")]

        public async Task<IActionResult> CrearPago(int idTarjeta, NuevoPago nuevoPago, EstadoCuenta estadoCuenta)
        {
            try
            {
                using (HttpClient client = _httpClientFactory.CreateClient())
                {
                    string apiUrl = "https://localhost:7165/api/Pago/AgregarPago";

                    var datosPago = new
                    {
                        TarjetaCreditoId = idTarjeta,
                        Monto = nuevoPago.Monto,
                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, datosPago);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Tarjeta");
                    }

                    else
                    {
                        return StatusCode((int)response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el pago: {ex.Message}");
            }


        }

        

    }
}
