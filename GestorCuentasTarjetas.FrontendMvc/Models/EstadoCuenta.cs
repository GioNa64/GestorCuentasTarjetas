using System.ComponentModel.DataAnnotations;

namespace GestorCuentasTarjetas.FrontendMvc.Models
{
    public class EstadoCuenta
    {
        public Tarjeta Tarjeta { get; set; }
        public List<Transaccion> Transacciones { get; set; }
    }



    public class Transaccion
    {
        public int IdTarjeta { get; set; }
        public string TitularNombre { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoDisponible { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }


        public decimal Monto { get; set; }
        public decimal? TotalCompras { get; set; }
    }
}
