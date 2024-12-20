using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            DetallePedidos = new HashSet<DetallePedido>();
            Envios = new HashSet<Envio>();
            HistorialPedidos = new HashSet<HistorialPedido>();
            TransaccionesPagos = new HashSet<TransaccionesPago>();
        }

        [Key]
        public int IdPedido { get; set; }
        public int? IdUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaPedido { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Total { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? MetodoEntrega { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? DireccionEntrega { get; set; }
        public int? IdSucursal { get; set; }

        [ForeignKey("IdSucursal")]
        [InverseProperty("Pedidos")]
        public virtual Sucursale? IdSucursalNavigation { get; set; }
        [ForeignKey("IdUsuario")]
        [InverseProperty("Pedidos")]
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        [InverseProperty("IdPedidoNavigation")]
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
        [InverseProperty("IdPedidoNavigation")]
        public virtual ICollection<Envio> Envios { get; set; }
        [InverseProperty("IdPedidoNavigation")]
        public virtual ICollection<HistorialPedido> HistorialPedidos { get; set; }
        [InverseProperty("IdPedidoNavigation")]
        public virtual ICollection<TransaccionesPago> TransaccionesPagos { get; set; }
    }
}
