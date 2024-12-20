using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class Oferta
    {
        [Key]
        public int IdOferta { get; set; }
        public int? IdProducto { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? PorcentajeDescuento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaFin { get; set; }

        [ForeignKey("IdProducto")]
        [InverseProperty("Oferta")]
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
