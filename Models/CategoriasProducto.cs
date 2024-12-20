using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class CategoriasProducto
    {
        public CategoriasProducto()
        {
            Productos = new HashSet<Producto>();
        }

        [Key]
        public int IdCategoria { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreCategoria { get; set; }
        [Column(TypeName = "text")]
        public string? Descripcion { get; set; }

        [InverseProperty("IdCategoriaNavigation")]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
