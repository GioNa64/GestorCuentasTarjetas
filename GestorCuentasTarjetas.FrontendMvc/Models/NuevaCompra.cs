using System.ComponentModel.DataAnnotations;

namespace GestorCuentasTarjetas.FrontendMvc.Models
{
    public class NuevaCompra
    {
        public int TarjetaCreditoId { get; set; }
        public string Descripcion { get; set; }

        public decimal Monto { get; set; }

    }
}

