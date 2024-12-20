using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    public partial class Role
    {
        public Role()
        {
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int IdRol { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NombreRol { get; set; }

        [InverseProperty("IdRolNavigation")]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
