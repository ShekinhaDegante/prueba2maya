using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class PedidosProveedore
    {
        public PedidosProveedore()
        {
            DetallePedidosProveedores = new HashSet<DetallePedidosProveedore>();
        }

        [Key]
        public int IdPedidoProveedor { get; set; }
        public int? IdProveedor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaPedido { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Estado { get; set; }

        [ForeignKey("IdProveedor")]
        [InverseProperty("PedidosProveedores")]
        public virtual Proveedore? IdProveedorNavigation { get; set; }
        [InverseProperty("IdPedidoProveedorNavigation")]
        public virtual ICollection<DetallePedidosProveedore> DetallePedidosProveedores { get; set; }
    }
}
