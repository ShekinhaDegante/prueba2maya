using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class HistorialPrecio
    {
        [Key]
        public int IdHistorial { get; set; }
        public int? IdProducto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PrecioAnterior { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PrecioNuevo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaCambio { get; set; }

        [ForeignKey("IdProducto")]
        [InverseProperty("HistorialPrecios")]
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
