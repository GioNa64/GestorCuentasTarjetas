using Microsoft.AspNetCore.Mvc;

using GestorCuentasTarjetas.FrontendMvc.Models;
namespace GestorCuentasTarjetas.FrontendMvc.Controllers
{
    public class TarjetaController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://localhost:7165/api/Tarjeta/Lista";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                   
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                   
                    
                    List<Tarjeta> tarjetas = await response.Content.ReadAsAsync<List<Tarjeta>>();

                    return View(tarjetas);
                }
                else
                {
                    // Retorna la vista con una lista vacía
                    return View(new List<Tarjeta>());
                }
            }
        }

        [HttpGet]
        [Route("ObtenerEstadoCuenta/{idTarjeta}")]
        public async Task<IActionResult> ObtenerEstadoCuenta(int idTarjeta)
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
                     

                        var transacciones = estadoCuentaResponse.Transacciones;

                        
                        var totalComprasPorMes = CalcularTotalComprasPorMes(transacciones);

                        ViewBag.TotalComprasPorMes = totalComprasPorMes;

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

        private Dictionary<string, decimal> CalcularTotalComprasPorMes(List<Transaccion> transacciones)
        {
            var totalComprasPorMes = new Dictionary<string, decimal>();

            foreach (var transaccion in transacciones)
            {
                if (transaccion.TipoTransaccion == "Compra")
                {
                    var clave = $"{transaccion.Fecha.Month}/{transaccion.Fecha.Year}";

                    if (totalComprasPorMes.ContainsKey(clave))
                    {
                        totalComprasPorMes[clave] += transaccion.Monto;
                    }
                    else
                    {
                        totalComprasPorMes[clave] = transaccion.Monto;
                    }
                }
            }

            return totalComprasPorMes;
        }

        [HttpGet]
        public async Task<ActionResult> BuscarPorNombre(string nombreTarjeta)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:7165/api/Tarjeta/BuscarPorNombre/{nombreTarjeta}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var resultados = await response.Content.ReadAsAsync<List<Tarjeta>>();

                        return View("Index", resultados);
                    }
                    else
                    {
                        return View("Index", new List<Tarjeta>());
                    }
                }
            }
            catch (Exception ex)
            {            
                Console.WriteLine($"Error en la búsqueda: {ex.Message}");
                return View("Index", new List<Tarjeta>());
            }

        }
    }

}

