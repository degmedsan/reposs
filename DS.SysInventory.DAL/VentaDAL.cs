﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.SysInventory.EN.Filtros;
using DS.SysInventory.EN;
using Microsoft.EntityFrameworkCore;

namespace DS.SysInventory.DAL
{
    public class VentaDAL
    {
        readonly SysInventoryDBContext dbContext;

        public VentaDAL(SysInventoryDBContext sysInventoryDB)
        {
            dbContext = sysInventoryDB;
        }

        public async Task<int> CrearAsync(Venta pVenta)
        {
            //Agega la venta con sus detalles
            dbContext.Ventas.Add(pVenta);
            int result = await dbContext.SaveChangesAsync();
            if (result > 0)
            {
                //Actualizar stock de productos
                foreach (var detalle in pVenta.DetalleVentas)
                {
                    var producto = await dbContext.Productos.FirstOrDefaultAsync(p => p.Id == detalle.IdProducto);
                    if (producto != null)
                    {
                        producto.CantidadDisponible -= detalle.Cantidad;
                    }
                    else
                    {
                        // Podrías lanzar una excepción, registrar un error, etc.
                        throw new InvalidOperationException("Stock insuficiente para el producto: " + producto.Nombre);
                    }
                }
            }
            return await dbContext.SaveChangesAsync();
        }
        public async Task<int> AnularAsync(int idVenta)
        {
            var venta = await dbContext.Ventas
                .Include(v => v.DetalleVentas)
                .FirstOrDefaultAsync(v => v.Id == idVenta);

            if (venta != null && venta.Estado != (byte)Venta.EnumEstadoVenta.Anulada)
            {
                //Marcar la venta como anulada 
                venta.Estado = (byte)Venta.EnumEstadoVenta.Anulada;

                //Restar la cantidad de los productos vendidos 
                foreach (var detalle in venta.DetalleVentas)
                {
                    var producto = await dbContext.Productos.FirstOrDefaultAsync(p => p.Id == detalle.IdProducto);
                    if (producto != null)
                    {
                        producto.CantidadDisponible += detalle.Cantidad;
                    }
                }

                return await dbContext.SaveChangesAsync();
            }

            return 0; // Si ya estaba nula no hace nada 
        }

        public async Task<Venta> ObtenerPorIdAsync(int idVenta)
        {
            var venta = await dbContext.Ventas
                .Include(v => v.DetalleVentas)
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == idVenta);

            return venta ?? new Venta();
        }

        public async Task<List<Venta>> ObtenerTodosAsync()
        {
            var ventas = await dbContext.Ventas
                .Include(v => v.DetalleVentas)
                .Include(v => v.Cliente).ToListAsync();
            return ventas ?? new List<Venta>();
        }
        public async Task<List<Venta>> ObtenerPorEstadoAsync(byte estado)
        {
            var ventasQuery = dbContext.Ventas.AsQueryable();

            if (estado != 0)
            {
                ventasQuery = ventasQuery.Where(v => v.Estado == estado);
            }

            ventasQuery = ventasQuery
                .Include(v => v.DetalleVentas)
                .Include(v => v.Cliente);

            var ventas = await ventasQuery.ToListAsync();

            return ventas ?? new List<Venta>();

        }
        public async Task<List<Venta>> ObtenerReporteVentasAsync(VentaFiltros filtro)
        {
            var ventasQuery = dbContext.Ventas
                .Include(v => v.DetalleVentas)
                    .ThenInclude(dv => dv.Producto)
                .Include(v => v.Cliente)
                .AsQueryable();

            if (filtro.FechaInicio.HasValue)
            {
                DateTime fechaInicio = filtro.FechaInicio.Value.Date; //Eliminar la hora, deja solo la fecha
                ventasQuery = ventasQuery.Where(v => v.FechaVenta >= fechaInicio);
            }

            if (filtro.FechaFin.HasValue)
            {
                DateTime fechaFin = filtro.FechaFin.Value.Date.AddDays(1).AddSeconds(-1); //incluirhasta  el final el dia
                ventasQuery = ventasQuery.Where(v => v.FechaVenta <= fechaFin);
            }

            return await ventasQuery.ToListAsync();
        }
    }
}
