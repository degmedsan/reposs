﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.SysInventory.DAL;
using DS.SysInventory.EN;

namespace DS.SysInventory.BL
{
    public class CompraBL
    {
        readonly CompraDAL compraDAL;

        public CompraBL(CompraDAL pCompraDAL)
        {
            compraDAL = pCompraDAL;
        }
        public async Task<int> CrearAsync (Compra pCompra)
        {
            return await compraDAL.CrearAsync (pCompra);
        }
        public async Task<int> AnularAsync(int idCompra)
        {
            return await compraDAL.AnularAsync (idCompra);
        }
        public async Task<Compra> ObtenerPorIdAsync(int idCompra)
        {
            return await compraDAL.ObtenerPorIdAsync (idCompra);
        }
        public async Task<List<Compra>> ObtenerTodosAsync()
        {
            return await compraDAL.ObtenerTodosAsync();
        }
        public async Task<List<Compra>> ObtenerPorEstadoAsync(byte estado)
        {
            return await compraDAL.ObtenerPorEstadoAsync (estado);
        }
    }
}
