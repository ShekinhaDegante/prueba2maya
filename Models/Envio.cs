using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class Envio
    {
        [Key]
        public int IdEnvio { get; set; }
        public int? IdPedido { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? DireccionEnvio { get; set; }
        public int? IdSucursalRetiro { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? EstadoEnvio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaEnvio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaEntrega { get; set; }

        [ForeignKey("IdPedido")]
        [InverseProperty("Envios")]
        public virtual Pedido? IdPedidoNavigation { get; set; }
        [ForeignKey("IdSucursalRetiro")]
        [InverseProperty("Envios")]
        public virtual Sucursale? IdSucursalRetiroNavigation { get; set; }
    }
}
