using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scavolini.Database.Models
{
    public partial class Label
    {
        public Label()
        {
            SuperLabels = new HashSet<SuperLabel>();
        }

        [Key]
        public int Id { get; set; }
        public int ComuneId { get; set; }
        [Column("Label")]
        [StringLength(100)]
        public string Label1 { get; set; } = null!;

        [ForeignKey(nameof(ComuneId))]
        [InverseProperty(nameof(Comuni.Labels))]
        public virtual Comuni Comune { get; set; } = null!;
        [InverseProperty(nameof(SuperLabel.Label))]
        public virtual ICollection<SuperLabel> SuperLabels { get; set; }
    }
}
