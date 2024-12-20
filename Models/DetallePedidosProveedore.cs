using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class DetallePedidosProveedore
    {
        [Key]
        public int IdDetallePedidoProveedor { get; set; }
        public int? IdPedidoProveedor { get; set; }
        public int? IdProducto { get; set; }
        public int? CantidadSolicitada { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PrecioUnitario { get; set; }

        [ForeignKey("IdPedidoProveedor")]
        [InverseProperty("DetallePedidosProveedores")]
        public virtual PedidosProveedore? IdPedidoProveedorNavigation { get; set; }
        [ForeignKey("IdProducto")]
        [InverseProperty("DetallePedidosProveedores")]
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
