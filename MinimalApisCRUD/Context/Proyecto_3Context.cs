using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MinimalApisCRUD.Models;

namespace MinimalApisCRUD.Context
{
    public partial class Proyecto_3Context : DbContext
    {
        public Proyecto_3Context()
        {
        }

        public Proyecto_3Context(DbContextOptions<Proyecto_3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Afiliado> Afiliados { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<Transaccion> Transaccions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:proyecto3-server.database.windows.net,1433;Initial Catalog=Proyecto_3;Persist Security Info=False;User ID=Administrador;Password=Admin_2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Afiliado>(entity =>
            {
                entity.HasKey(e => e.IdAfiliado)
                    .HasName("PK__afiliado__F7566A96441F959F");

                entity.ToTable("afiliado");

                entity.Property(e => e.IdAfiliado).HasColumnName("id_afiliado");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaIniciocobertura)
                    .HasColumnType("date")
                    .HasColumnName("fechaIniciocobertura");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.MontoCobertura)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("montoCobertura");

                entity.Property(e => e.NoTelefono).HasColumnName("noTelefono");

                entity.Property(e => e.PApellido)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("pApellido");

                entity.Property(e => e.PNombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("pNombre");

                entity.Property(e => e.SApellido)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("sApellido");

                entity.Property(e => e.SNombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("sNombre");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__proveedo__8D3DFE280E5BF382");

                entity.ToTable("proveedor");

                entity.HasIndex(e => e.Nit, "UQ__proveedo__DF97D0E407DF8E60")
                    .IsUnique();

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Nit).HasColumnName("nit");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("razonSocial");
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(e => e.IdTransaccion)
                    .HasName("PK__transacc__1EDAC09956FAFBF7");

                entity.ToTable("transaccion");

                entity.Property(e => e.IdTransaccion).HasColumnName("id_transaccion");

                entity.Property(e => e.FechaCobertura)
                    .HasColumnType("date")
                    .HasColumnName("fecha_cobertura");

                entity.Property(e => e.FechaConsulta)
                    .HasColumnType("date")
                    .HasColumnName("fecha_consulta");

                entity.Property(e => e.IdAfiliado).HasColumnName("id_afiliado");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.Respuesta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("respuesta");

                entity.HasOne(d => d.IdAfiliadoNavigation)
                    .WithMany(p => p.Transaccions)
                    .HasForeignKey(d => d.IdAfiliado)
                    .HasConstraintName("fk_idAfiliado_idP");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Transaccions)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("fk_idProveedor_idP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
