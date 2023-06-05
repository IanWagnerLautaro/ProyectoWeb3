using System;
using System.Collections.Generic;
using GrupoLogin.DATA.Model;
using Microsoft.EntityFrameworkCore;

namespace GrupoLogin.DATA;

public partial class GrupoLoginContext : DbContext
{
    public DbSet<Producto> Producto { get; set; }
    public GrupoLoginContext()
    {
    }

    public GrupoLoginContext(DbContextOptions<GrupoLoginContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-UTDPFOT\\SQLEXPRESS;trusted_connection=true;Database=GrupoLogin;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Producto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
