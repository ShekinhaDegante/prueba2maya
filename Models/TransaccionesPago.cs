using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class TransaccionesPago
    {
        [Key]
        public int IdTransaccion { get; set; }
        public int? IdPedido { get; set; }
        public int? IdModoPago { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Monto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaTransaccion { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Estado { get; set; }

        [ForeignKey("IdModoPago")]
        [InverseProperty("TransaccionesPagos")]
        public virtual ModosPago? IdModoPagoNavigation { get; set; }
        [ForeignKey("IdPedido")]
        [InverseProperty("TransaccionesPagos")]
        public virtual Pedido? IdPedidoNavigation { get; set; }
    }
}
