using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosSuplidores_Blazor.Models
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }

        [Required(ErrorMessage = "Seleccione una fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Seleccione un suplidor")]
        public int SuplidorId { get; set; }

        [Required(ErrorMessage = "Ingrese al menos un producto")]
        [ForeignKey("OrdenId")]

        public virtual List<OrdenesDetalle> Detalle { get; set; } = new List<OrdenesDetalle>();
    }
}
