using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    [Index("Email", Name = "UQ__Usuarios__A9D10534F58AAA08", IsUnique = true)]
    public partial class Usuario
    {
        public Usuario()
        {
            AuditoriaInventarios = new HashSet<AuditoriaInventario>();
            CarritoVenta = new HashSet<CarritoVenta>();
            ClientesFrecuentes = new HashSet<ClientesFrecuente>();
            HistorialPedidos = new HashSet<HistorialPedido>();
            Pedidos = new HashSet<Pedido>();
        }

        [Key]
        public int IdUsuario { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreUsuario { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Contrasena { get; set; }
        public int? IdRol { get; set; }

        [ForeignKey("IdRol")]
        [InverseProperty("Usuarios")]
        public virtual Role? IdRolNavigation { get; set; }
        [InverseProperty("RealizadoPorNavigation")]
        public virtual ICollection<AuditoriaInventario> AuditoriaInventarios { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<CarritoVenta> CarritoVenta { get; set; }
        [InverseProperty("IdClienteNavigation")]
        public virtual ICollection<ClientesFrecuente> ClientesFrecuentes { get; set; }
        [InverseProperty("ActualizadoPorNavigation")]
        public virtual ICollection<HistorialPedido> HistorialPedidos { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
