using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scavolini.Database.Models
{
    [Table("Comuni")]
    public partial class Comuni
    {
        public Comuni()
        {
            Labels = new HashSet<Label>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Nome { get; set; } = null!;
        [StringLength(50)]
        public string CodiceRegione { get; set; } = null!;
        [StringLength(10)]
        public string Cap { get; set; } = null!;

        [InverseProperty(nameof(Label.Comune))]
        public virtual ICollection<Label> Labels { get; set; }
    }
}
