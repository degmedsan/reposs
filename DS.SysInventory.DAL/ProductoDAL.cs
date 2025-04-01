﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.SysInventory.EN;
using Microsoft.EntityFrameworkCore;

namespace DS.SysInventory.DAL
{
    public class ProductoDAL
    {
        readonly SysInventoryDBContext _dbContext;
        public ProductoDAL(SysInventoryDBContext context)
        {
            _dbContext = context;
        }
        public async Task<int> CrearAsync(Producto pProducto)
        {
            Producto producto = new Producto()
            {
                Id = pProducto.Id,
                Nombre = pProducto.Nombre,
                Precio = pProducto.Precio,  
                CantidadDisponible = pProducto.CantidadDisponible,
                FechaCreacion = pProducto.FechaCreacion,
            };
            _dbContext.Add(producto);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> EliminarAsync(Producto pProducto)
        {
            var producto = _dbContext.Productos.FirstOrDefault(s => s.Id == pProducto.Id);
            if (producto != null)
            {
                _dbContext.Productos.Remove(producto);
                return await _dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }
        public async Task<int> ModificarAsync(Producto pProducto)
        {
            var producto = _dbContext.Productos.FirstOrDefault(s => s.Id == pProducto.Id);
            if (producto != null && producto.Id > 0)
            {
                producto.Nombre = pProducto.Nombre;
                producto.Precio = pProducto.Precio;
                producto.CantidadDisponible = pProducto.CantidadDisponible;
                producto.FechaCreacion = pProducto.FechaCreacion;
                return await _dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }
        public async Task<Producto> ObtenerPorIdAsync (Producto pProducto)
        {
            var producto = await _dbContext.Productos.FirstOrDefaultAsync(s => s.Id == pProducto.Id);
            if (producto != null && producto.Id != 0)
            {
                return new Producto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    CantidadDisponible = producto.CantidadDisponible,
                    FechaCreacion = producto.FechaCreacion,
                };
            }
            else
                return new Producto();
        }
        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            var productos = await _dbContext.Productos.ToListAsync();
            if (productos != null && productos.Count > 0)
            {
                var list = new List<Producto>();
                productos.ForEach(s => list.Add(new Producto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Precio = s.Precio,
                    CantidadDisponible = s.CantidadDisponible,
                    FechaCreacion = s.FechaCreacion,
                }));
                return list;
            }
            else
                return new List<Producto>();
        }
        public async Task AgregarTodosAsync(List<Producto> pProductos)
        {
            await _dbContext.Productos.AddRangeAsync(pProductos);
            await _dbContext.SaveChangesAsync();
        }
    }
}
