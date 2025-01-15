﻿using CrudAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Perfil>(tb =>
            {
                tb.HasKey(col => col.idPerfil);
                tb.Property(col => col.idPerfil).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).HasMaxLength(50);
                tb.ToTable("Perfil");
                tb.HasData(
                    new Perfil { idPerfil = 1, Nombre = "Programador Dev" },
                    new Perfil { idPerfil = 2, Nombre = "Programador Senior" },
                    new Perfil { idPerfil = 3, Nombre = "Analista" }
                    );
            });


            modelBuilder.Entity<Empleado>(tb =>
            {
                tb.HasKey(col => col.IdEmpleado);
                tb.Property(col => col.IdEmpleado).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.NombreCompleto).HasMaxLength(50);
                tb.HasOne(col => col.PerfilReferencia).WithMany(p => p.EmpleadosReferencia)
                .HasForeignKey(col => col.IdPerfil);
                tb.ToTable("Empleado");

            });
        }
    }
}
