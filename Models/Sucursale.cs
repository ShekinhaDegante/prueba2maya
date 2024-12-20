using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class Sucursale
    {
        public Sucursale()
        {
            Envios = new HashSet<Envio>();
            InventarioSucursals = new HashSet<InventarioSucursal>();
            Pedidos = new HashSet<Pedido>();
            Productos = new HashSet<Producto>();
        }

        [Key]
        public int IdSucursal { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreSucursal { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Direccion { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string? Telefono { get; set; }

        [InverseProperty("IdSucursalRetiroNavigation")]
        public virtual ICollection<Envio> Envios { get; set; }
        [InverseProperty("IdSucursalNavigation")]
        public virtual ICollection<InventarioSucursal> InventarioSucursals { get; set; }
        [InverseProperty("IdSucursalNavigation")]
        public virtual ICollection<Pedido> Pedidos { get; set; }
        [InverseProperty("IdSucursalNavigation")]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
