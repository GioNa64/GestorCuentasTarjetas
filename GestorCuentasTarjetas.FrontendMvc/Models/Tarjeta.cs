using System.ComponentModel.DataAnnotations;

namespace GestorCuentasTarjetas.FrontendMvc.Models
{
    public class Tarjeta
    {
        [Key]
        public int IdTarjeta { get; set; }
        public string TitularNombre { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoDisponible { get; set; }
    }

}
