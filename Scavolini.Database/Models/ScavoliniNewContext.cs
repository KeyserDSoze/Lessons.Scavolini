using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scavolini.Database.Models
{
    public partial class ScavoliniNewContext : DbContext
    {
        public ScavoliniNewContext()
        {
        }

        public ScavoliniNewContext(DbContextOptions<ScavoliniNewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comuni> Comunis { get; set; } = null!;
        public virtual DbSet<Label> Labels { get; set; } = null!;
        public virtual DbSet<SuperLabel> SuperLabels { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:{insertserver};Initial Catalog=scavolini;Persist Security Info=True;User ID={insertusername};Password={insertpasswordhere};MultipleActiveResultSets=True;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comuni>(entity =>
            {
                entity.Property(e => e.Cap).IsFixedLength();
            });

            modelBuilder.Entity<Label>(entity =>
            {
                entity.Property(e => e.Label1).IsFixedLength();

                entity.HasOne(d => d.Comune)
                    .WithMany(p => p.Labels)
                    .HasForeignKey(d => d.ComuneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Labels_Comuni");
            });

            modelBuilder.Entity<SuperLabel>(entity =>
            {
                entity.Property(e => e.SuperLabel1).IsFixedLength();

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.SuperLabels)
                    .HasForeignKey(d => d.LabelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuperLabels_Labels");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
