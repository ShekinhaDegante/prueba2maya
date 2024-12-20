using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    [Table("AuditoriaInventario")]
    public partial class AuditoriaInventario
    {
        [Key]
        public int IdAuditoria { get; set; }
        public int? IdProducto { get; set; }
        public int? CantidadAntes { get; set; }
        public int? CantidadDespues { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaAuditoria { get; set; }
        public int? RealizadoPor { get; set; }

        [ForeignKey("IdProducto")]
        [InverseProperty("AuditoriaInventarios")]
        public virtual Producto? IdProductoNavigation { get; set; }
        [ForeignKey("RealizadoPor")]
        [InverseProperty("AuditoriaInventarios")]
        public virtual Usuario? RealizadoPorNavigation { get; set; }
    }
}
