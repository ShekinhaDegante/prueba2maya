using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    [Table("InventarioSucursal")]
    public partial class InventarioSucursal
    {
        [Key]
        public int IdInventario { get; set; }
        public int? IdProducto { get; set; }
        public int? IdSucursal { get; set; }
        public int? Cantidad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaActualizacion { get; set; }

        [ForeignKey("IdProducto")]
        [InverseProperty("InventarioSucursals")]
        public virtual Producto? IdProductoNavigation { get; set; }
        [ForeignKey("IdSucursal")]
        [InverseProperty("InventarioSucursals")]
        public virtual Sucursale? IdSucursalNavigation { get; set; }
    }
}
