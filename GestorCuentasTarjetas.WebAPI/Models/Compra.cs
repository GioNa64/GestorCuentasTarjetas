using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorCuentasTarjetas.WebAPI.Models
{
    public class Compra
    {
        [Key]
        public int CompraId { get; set; }
        [ForeignKey("Tarjeta")]
        [Required]
        public int TarjetaCreditoId { get; set; }
        public DateTime FechaCompra { get; set; }
        public string Descripcion { get; set; }


        public decimal Monto { get; set; }

        [ValidateNever]
        public Tarjeta TarjetaCredito { get; set; }  
    }
}