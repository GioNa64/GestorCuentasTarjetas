using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestorCuentasTarjetas.FrontendMvc.Models;

namespace GestorCuentasTarjetas.FrontendMvc.Controllers
{
    public class CompraController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CompraController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        [Route("CrearCompra/{idTarjeta}")]
        public IActionResult CrearCompra(int idTarjeta)
        {
            return View();
        }



        [HttpPost]
        [Route("CrearCompra/{idTarjeta}")]

        public async Task<IActionResult> CrearCompra(int idTarjeta, NuevaCompra nuevaCompra)
        {
            try
            {
                using (HttpClient client = _httpClientFactory.CreateClient())
                {
                    string apiUrl = "https://localhost:7165/api/Compra/CrearCompra";

                    var datosCompra = new
                    {
                        TarjetaCreditoId = idTarjeta,
                        Monto = nuevaCompra.Monto,
                        Descripcion = nuevaCompra.Descripcion
                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, datosCompra);

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
                return StatusCode(500, $"Error al crear la compra: {ex.Message}");
            }
        }
    }
}
    