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
    public class RepositoryEvaluacion : IRepositoryEvaluacion
    {
        public async Task<Evaluacion> Add(Evaluacion eva)
        {
            int rows1 = 0;
            try
            {
                Evaluacion evaluacion = null;
                if (eva != null)
                {
                   evaluacion = eva;
                   using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        ctx.Compra.Attach(eva.Compra);
                        ctx.Tienda.Attach(eva.Tienda);
                        ctx.Usuario.Attach(eva.Usuario);
                        ctx.Evaluacion.Add(evaluacion);
                        rows1 = await ctx.SaveChangesAsync();
                    }
                }

                return evaluacion;
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

        public async Task<Evaluacion> Edit(Evaluacion evaluacion)
        {
            int rows1 = 0;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    if (evaluacion != null)
                    {
                        ctx.Entry(evaluacion).State = EntityState.Modified;
                        rows1 = await ctx.SaveChangesAsync();
                    }
                }
                return evaluacion;

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

        public async Task<IEnumerable<Evaluacion>> GetAll()
        {
            try
            {

                IEnumerable<Evaluacion> evaluaciones = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    evaluaciones = await ctx.Evaluacion.ToListAsync();

                }
                return evaluaciones;

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

        public async Task<IEnumerable<Evaluacion>> GetByClient(int idClient)
        {
            try
            {
                IEnumerable<Evaluacion> evaluaciones = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    evaluaciones = await ctx.Evaluacion.Where(e=> e.idCliente==idClient).ToListAsync();

                   
                }
                return evaluaciones;
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

        public  async Task<Evaluacion> GetById(int id)
        {
            try
            {
                Evaluacion evaluacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    evaluacion = ctx.Evaluacion.Find(id);

                    evaluacion = await ctx.Evaluacion.Where(p => p.Id == id).Include("Compra")
                        .Include("Usuario")
                        .Include("Tienda").FirstOrDefaultAsync();
                }
                return evaluacion;
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

        public async Task<IEnumerable<Evaluacion>> GetBySeller(int idSeller)
        {
            try
            {
                IEnumerable<Evaluacion> evaluaciones = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    evaluaciones = await ctx.Evaluacion.Where(e => e.idVendedor == idSeller).ToListAsync();


                }
                return evaluaciones;
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

        public async Task<IEnumerable<Producto>> GetTop5Products()
        {
            try
            {
                IEnumerable<Producto> topProductos = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    topProductos = await ctx.Producto
                .Select(producto => new
                {
                    Producto = producto,
                    PromedioCalificaciones = producto.DetalleCompra
                        .SelectMany(dc => dc.Compra.Evaluacion)
                        .Average(e => (double)(e.calificacionACliente + e.calificacionAVendedor) / 2)
                })
                .OrderByDescending(p => p.PromedioCalificaciones)
                .Take(5)
                .Select(p => p.Producto) // Recuperar la entidad Producto
                .ToListAsync();



                }
                return topProductos;
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

        public async Task<IEnumerable<Tienda>> GetTop5Sellers()
        {
            try
            {
                IEnumerable<Tienda> topTiendas = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    topTiendas = await ctx.Tienda
                .Select(tienda => new
                {
                    Tienda = tienda,
                    PromedioCalificaciones = tienda.Evaluacion
                        .Average(e => (double)(e.calificacionACliente + e.calificacionAVendedor) / 2)
                })
                .OrderByDescending(t => t.PromedioCalificaciones)
                .Take(5)
                .Select(t => t.Tienda) // Recuperar la entidad Tienda
                .ToListAsync();



                }
                return topTiendas;
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

        public async Task<IEnumerable<Tienda>> GetTop3TiendasPeorEvaluadas()
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var topTiendas = await ctx.Tienda
                        .Select(tienda => new
                        {
                            Tienda = tienda,
                            PromedioCalificaciones = tienda.Evaluacion
                                .Average(e => (double)(e.calificacionACliente + e.calificacionAVendedor) / 2)
                        })
                        .OrderBy(p => p.PromedioCalificaciones)
                        .Take(3)
                        .Select(p => p.Tienda) // Recuperar la entidad Tienda
                        .ToListAsync();

                    return topTiendas;
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


    }
}
