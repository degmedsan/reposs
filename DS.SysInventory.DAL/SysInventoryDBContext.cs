using Microsoft.EntityFrameworkCore;
using DS.SysInventory.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.SysInventory.DAL
{
    public class SysInventoryDBContext: DbContext
    {
        public SysInventoryDBContext(DbContextOptions<SysInventoryDBContext> options) : base(options) 
        {
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set;  }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get;set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleCompra>()
                .HasOne(d => d.Compra)
                .WithMany(c => c.DetalleCompras)
                .HasForeignKey(d => d.IdCompra);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.DetalleVentas)
                .HasForeignKey(d => d.IdVenta);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.IdProducto);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany()
                .HasForeignKey(d => d.IdCliente);

            // Configuraciones de propiedades
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(v => v.Total).HasColumnType("decimal(18,2)");
                entity.Property(v => v.FechaVenta).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.Property(d => d.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(d => d.SubTotal).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.Property(d => d.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(d => d.SubTotal).HasColumnType("decimal(18,2)");
            });
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
