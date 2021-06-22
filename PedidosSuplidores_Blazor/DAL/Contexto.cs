using Microsoft.EntityFrameworkCore;
using PedidosSuplidores_Blazor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosSuplidores_Blazor.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Suplidores> Suplidores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source = Data/ComprasSuplidor.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>().HasData(new List<Productos>()
            {
                new Productos() { ProductoId = 1, Descripcion = "Arroz Selecto", Costo = 23, Inventario = 100},
                new Productos() { ProductoId = 2, Descripcion = "Habichuelas rojas", Costo = 50, Inventario = 40},
                new Productos() { ProductoId = 3, Descripcion = "CocaCola", Costo = 30, Inventario = 15},
                new Productos() { ProductoId = 4, Descripcion = "Galletas Oreo", Costo = 8, Inventario = 24},
                new Productos() { ProductoId = 5, Descripcion = "Doritos Rojos", Costo = 20, Inventario = 30},
                new Productos() { ProductoId = 6, Descripcion = "Pepsi", Costo = 25, Inventario = 150}
            });

            modelBuilder.Entity<Suplidores>().HasData(new List<Suplidores>()
            {
                new Suplidores() { SuplidorId = 1, Nombres = "Eufemio Vargas" }
            });
        }

    }
}
