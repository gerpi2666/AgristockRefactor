﻿using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryCompra : IRepositoryCompra
    {
        public Compra GetCompraById(int id)
        {

            try
            {
                Compra compra = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    compra = ctx.Compra.Find(id);
                    compra = ctx.Compra.Where(l => l.Id == id)
                        .Include(u => u.DetalleCompra)
                        .Include(X => X.DetalleCompra.Select(d => d.Producto))
                        .Include(p => p.DetalleCompra.Select(s => s.Producto.Tienda))
                        .Include(k => k.Usuario)
                        .Include(h=>h.Usuario.MetodoPago).FirstOrDefault();
                   
                }
                return compra;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Compra> GetCompras()
        {
            try
            {
                IEnumerable<Compra> Compras = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Compras = ctx.Compra.Include("DetalleCompra").ToList<Compra>();

                }
                return Compras;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Compra> GetComprasByCliente(int idCliente)
        {

            try
            {
                IEnumerable<Compra> compra = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                         compra = ctx.Compra.Where(l => l.IdUsuario == idCliente)
                        .Include(u => u.DetalleCompra)
                        .Include(X => X.DetalleCompra.Select(d => d.Producto))
                        .Include(p => p.DetalleCompra.Select(s => s.Producto.Tienda))
                        .Include(k => k.Usuario)
                        .Include(h => h.Usuario.MetodoPago).ToList();

                }
                return compra;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Compra> GetComprasByTienda(int idTienda)
        {

            try
            {
                IEnumerable<Compra> compra = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    compra = ctx.Compra.Where(l => l.DetalleCompra.Any(d=>d.Producto.Tienda.Id== idTienda))
                   .Include(u => u.DetalleCompra)
                   .Include(X => X.DetalleCompra.Select(d => d.Producto))
                   .Include(p => p.DetalleCompra.Select(s => s.Producto.Tienda))
                   .Include(k => k.Usuario)
                   .Include(h => h.Usuario.MetodoPago).ToList();

                }
                return compra;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public async Task<Compra> Crear(Compra compra)
        {
            int rows1 = 0;
            try
            {
                    Compra comp = null;
                if (compra != null)
                {
                    // producto.IdProveedor = 1;
                    comp = compra;
                    comp.Activo = true;
                    comp.Borrado = false;
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        ctx.Compra.Add(comp);
                        rows1 = await ctx.SaveChangesAsync();
                    }
                }

                return comp;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

    }
}
