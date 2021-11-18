using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scavolini.Database.Models
{
    public partial class SuperLabel
    {
        [Key]
        public int Id { get; set; }
        public int LabelId { get; set; }
        [Column("SuperLabel")]
        [StringLength(100)]
        public string SuperLabel1 { get; set; } = null!;

        [ForeignKey(nameof(LabelId))]
        [InverseProperty("SuperLabels")]
        public virtual Label Label { get; set; } = null!;
    }
}
