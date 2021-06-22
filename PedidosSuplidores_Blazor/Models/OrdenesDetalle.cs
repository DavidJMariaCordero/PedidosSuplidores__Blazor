using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosSuplidores_Blazor.Models
{
    public class OrdenesDetalle
    {
        [Key]
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public OrdenesDetalle(int ordenId, int productoId, decimal cantidad, decimal costo)
        {
            this.OrdenId = ordenId;
            this.ProductoId = productoId;
            this.Cantidad = cantidad;
            this.Costo = costo;
        }
    }
}
