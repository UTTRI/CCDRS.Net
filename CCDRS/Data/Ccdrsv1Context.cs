using System;
using System.Collections.Generic;
using CCDRS.Model;
using Microsoft.EntityFrameworkCore;

namespace CCDRS.Data;

public partial class Ccdrsv1Context : DbContext
{
    public Ccdrsv1Context()
    {
    }

    public Ccdrsv1Context(DbContextOptions<Ccdrsv1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Direction> Directions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Direction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("direction_pkey");

            entity.ToTable("direction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abbr)
                .HasMaxLength(1)
                .HasColumnName("abbr");
            entity.Property(e => e.Compass).HasColumnName("compass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
