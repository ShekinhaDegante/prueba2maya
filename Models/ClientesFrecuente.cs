using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class ClientesFrecuente
    {
        [Key]
        public int IdClienteFrecuente { get; set; }
        public int? IdCliente { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? TotalCompras { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaUltimaCompra { get; set; }
        public int? PuntosAcumulados { get; set; }

        [ForeignKey("IdCliente")]
        [InverseProperty("ClientesFrecuentes")]
        public virtual Usuario? IdClienteNavigation { get; set; }
    }
}
