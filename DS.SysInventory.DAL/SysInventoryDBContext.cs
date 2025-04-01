﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleCompra>()
                .HasOne(d => d.Compra)
                .WithMany(c => c.DetalleCompras)
                .HasForeignKey(d => d.IdCompra);

            base.OnModelCreating(modelBuilder);
        }
    }
}
