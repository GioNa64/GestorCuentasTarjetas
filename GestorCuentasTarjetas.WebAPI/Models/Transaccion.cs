namespace GestorCuentasTarjetas.WebAPI.Models
{
    public class Transaccion
    {
        public string TipoTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public decimal? TotalCompras { get; set; }

    }
}
