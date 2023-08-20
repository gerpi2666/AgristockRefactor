using Infraestructure.Models;
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

        public async Task<Compra> Crear(Compra compra, List<DetalleCompra> carrito)
        {
            int rows1 = 0;
            try
            {
                    Compra comp = null;
                if (compra != null)
                {
                    comp = compra;
                    comp.Activo = true;
                    comp.Borrado = false;
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;

                        if (carrito !=null)
                        {
                            Compra refCom = getLastOne();
                            comp.DetalleCompra = new List<DetalleCompra>();
                            foreach (var detalle in carrito)
                            {
                                // ctx.DetalleCompra.Attach(detalle); 
                                detalle.IdCompra = refCom.Id + 1;
                                comp.DetalleCompra.Add(detalle);


                            }
                        }

                        ctx.Usuario.Attach(comp.Usuario);
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

        private int ChangeStateCompra(ICollection<DetalleCompra> detalleCompra)
        {
            int counter = 0;
            bool a = false;
            foreach (var item in detalleCompra)
            {
                if (item.Estado == true) 
                {
                    counter++;
                }
            }

            if(counter== detalleCompra.Count)
            {
                return 3;
            }
            else
            {
                if (counter==0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
              
            }                
                     
        }

        public void ChangeStateDetail(int idCompra,int idProducto)
        {
            try
            {

                Compra comp = GetCompraById(idCompra);
                DetalleCompra detalle = new DetalleCompra();
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        detalle= ctx.DetalleCompra.Where(p => p.idProducto == idProducto).FirstOrDefault();


                    detalle.Estado = true;
                    ctx.Entry(detalle).State = EntityState.Modified;
                    ctx.SaveChanges();

                    }
               

               
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

        public async Task<Compra> Actualizar(Compra compra)
        {
            int rows1 = 0;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    if (compra != null)
                    {
                        compra.Estado = ChangeStateCompra(compra.DetalleCompra);
                        ctx.Entry(compra).State = EntityState.Modified;
                        rows1 = await ctx.SaveChangesAsync();
                    }
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

        public async Task Delete(int id)
        {
            try
            {
                Compra compra = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    compra = await ctx.Compra.FindAsync(id);
                    if (compra != null)
                    {
                        compra.Borrado = true;
                        await ctx.SaveChangesAsync();
                    }
                }

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

        private Compra getLastOne()
        {
            try
            {
                Compra compra = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    compra = ctx.Compra.OrderByDescending(r => r.Id).FirstOrDefault();
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


        public int GetCompraCountToday()
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    DateTime currentDate = DateTime.Today;

                    int cantidadCompras = ctx.Compra
                        .Count(dcp => dcp.FechaHora.HasValue &&
                                     dcp.FechaHora.Value.Month == currentDate.Month &&
                                     dcp.FechaHora.Value.Year == currentDate.Year);

                    return cantidadCompras;
                }
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
                throw new Exception(mensaje);
            }
        }


        public void GetTopProductosCompradosMes(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    DateTime inicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

                    var currentDate = DateTime.Today;
                    var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

                    var resultado = ctx.DetalleCompra
                                       .Join(ctx.Producto, dc => dc.idProducto, p => p.Id, (dc, p) => new { DetalleCompra = dc, Producto = p })
                                       .Join(ctx.Compra, dcp => dcp.DetalleCompra.IdCompra, c => c.Id, (dcp, c) => new { DetalleCompraProducto = dcp, Compra = c })
                                       .Where(dcp => dcp.Compra.FechaHora.HasValue &&
                                                     dcp.Compra.FechaHora.Value.Month == currentDate.Month &&
                                                     dcp.Compra.FechaHora.Value.Year == currentDate.Year)
                                       .GroupBy(dcp => new { dcp.DetalleCompraProducto.Producto.Id, dcp.DetalleCompraProducto.Producto.Nombre })
                                       .Select(grp => new {
                                           ProductoId = grp.Key.Id,
                                           ProductoNombre = grp.Key.Nombre,
                                           CantidadTotalComprada = grp.Sum(dcp => dcp.DetalleCompraProducto.DetalleCompra.Cantidad)
                                       })
                                       .OrderByDescending(item => item.CantidadTotalComprada)
                                       .Take(5)
                                       .ToList();

                    foreach (var item in resultado)
                    {
                        var producto = ctx.Producto.FirstOrDefault(p => p.Id == item.ProductoId);
                        if (producto != null)
                        {
                            varEtiquetas += producto.Nombre + ",";
                            varValores += item.CantidadTotalComprada + ",";
                        }
                    }
                }

                // Eliminar la última coma de las cadenas
                if (!string.IsNullOrEmpty(varEtiquetas))
                    varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1);

                if (!string.IsNullOrEmpty(varValores))
                    varValores = varValores.Substring(0, varValores.Length - 1);

                // Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
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
                throw new Exception(mensaje);
            }
        }

    }
}
