using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            PedidosProveedores = new HashSet<PedidosProveedore>();
            Productos = new HashSet<Producto>();
        }

        [Key]
        public int IdProveedor { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreProveedor { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string? Telefono { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Direccion { get; set; }

        [InverseProperty("IdProveedorNavigation")]
        public virtual ICollection<PedidosProveedore> PedidosProveedores { get; set; }
        [InverseProperty("IdProveedorNavigation")]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
