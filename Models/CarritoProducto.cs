using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class CarritoProducto
    {
        [Key]
        public int IdCarritoProducto { get; set; }
        public int? IdCarrito { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }

        [ForeignKey("IdCarrito")]
        [InverseProperty("CarritoProductos")]
        public virtual CarritoVenta? IdCarritoNavigation { get; set; }
        [ForeignKey("IdProducto")]
        [InverseProperty("CarritoProductos")]
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
