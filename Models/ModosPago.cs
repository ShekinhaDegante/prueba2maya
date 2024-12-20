using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prueba2maya.Models
{
    [Table("ModosPago")]
    public partial class ModosPago
    {
        public ModosPago()
        {
            TransaccionesPagos = new HashSet<TransaccionesPago>();
        }

        [Key]
        public int IdModoPago { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Descripcion { get; set; }

        [InverseProperty("IdModoPagoNavigation")]
        public virtual ICollection<TransaccionesPago> TransaccionesPagos { get; set; }
    }
}
