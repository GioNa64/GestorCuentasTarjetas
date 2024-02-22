using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;


namespace GestorCuentasTarjetas.WebAPI.Models
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

        [JsonIgnore]
        public List<Compra> Compras { get; set; }

        [JsonIgnore]
        public List<Pago> Pagos { get; set; }
    }

}
