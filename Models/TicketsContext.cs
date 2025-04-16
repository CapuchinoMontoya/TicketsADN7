﻿using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;

namespace TicketsADN7.Models;

public partial class TicketsContext : DbContext
{
    public TicketsContext()
    {
    }

    public TicketsContext(DbContextOptions<TicketsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");

            entity.Property(e => e.UsuarioID);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RolID);
            entity.Property(e => e.DepartamentoID);
            entity.Property(e => e.Activo);
            entity.Property(e => e.Telefono).HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<INTELISIS.APPCORE.EL.Rol> Rol { get; set; } = default!;

public DbSet<INTELISIS.APPCORE.EL.Departamento> Departamento { get; set; } = default!;
}
