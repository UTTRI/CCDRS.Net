using System;
using System.Collections.Generic;
using CCDRS.Model;
using Microsoft.EntityFrameworkCore;

namespace CCDRS.Data;

public partial class TempContext : DbContext
{
    public TempContext()
    {
    }

    public TempContext(DbContextOptions<TempContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Screenline> Screenlines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ccdrsv1;User Id=postgres;Password=test123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Screenline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("screenline_pkey");

            entity.ToTable("screenline");

            entity.HasIndex(e => new { e.RegionId, e.SlineCode }, "screenline_region_id_sline_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.SlineCode).HasColumnName("sline_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
