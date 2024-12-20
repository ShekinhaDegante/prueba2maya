using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class Producto
    {
        public Producto()
        {
            AuditoriaInventarios = new HashSet<AuditoriaInventario>();
            CarritoProductos = new HashSet<CarritoProducto>();
            DetallePedidos = new HashSet<DetallePedido>();
            DetallePedidosProveedores = new HashSet<DetallePedidosProveedore>();
            HistorialPrecios = new HashSet<HistorialPrecio>();
            InventarioSucursals = new HashSet<InventarioSucursal>();
            Oferta = new HashSet<Oferta>();
        }

        [Key]
        public int IdProducto { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreProducto { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Descripcion { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Precio { get; set; }
        public int? CantidadDisponible { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdSucursal { get; set; }
        public int? IdProveedor { get; set; }
        [StringLength(255)]
        public string? ImagenUrl { get; set; }

        [ForeignKey("IdCategoria")]
        [InverseProperty("Productos")]
        public virtual CategoriasProducto? IdCategoriaNavigation { get; set; }
        [ForeignKey("IdProveedor")]
        [InverseProperty("Productos")]
        public virtual Proveedore? IdProveedorNavigation { get; set; }
        [ForeignKey("IdSucursal")]
        [InverseProperty("Productos")]
        public virtual Sucursale? IdSucursalNavigation { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<AuditoriaInventario> AuditoriaInventarios { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<DetallePedidosProveedore> DetallePedidosProveedores { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<HistorialPrecio> HistorialPrecios { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<InventarioSucursal> InventarioSucursals { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
