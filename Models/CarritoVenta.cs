using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class CarritoVenta
    {
        public CarritoVenta()
        {
            CarritoProductos = new HashSet<CarritoProducto>();
        }

        [Key]
        public int IdCarrito { get; set; }
        public int? IdUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("CarritoVenta")]
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        [InverseProperty("IdCarritoNavigation")]
        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}
