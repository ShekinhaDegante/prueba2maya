using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class HistorialPedido
    {
        [Key]
        public int IdHistorialPedido { get; set; }
        public int? IdPedido { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? EstadoAnterior { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? EstadoNuevo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaCambio { get; set; }
        public int? ActualizadoPor { get; set; }

        [ForeignKey("ActualizadoPor")]
        [InverseProperty("HistorialPedidos")]
        public virtual Usuario? ActualizadoPorNavigation { get; set; }
        [ForeignKey("IdPedido")]
        [InverseProperty("HistorialPedidos")]
        public virtual Pedido? IdPedidoNavigation { get; set; }
    }
}
