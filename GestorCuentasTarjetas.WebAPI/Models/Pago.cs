using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorCuentasTarjetas.WebAPI.Models
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }
        [ForeignKey("Tarjeta")]
        [Required]
        public int TarjetaCreditoId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }

        
        [ValidateNever]
        public Tarjeta TarjetaCredito { get; set; }

    }
}
