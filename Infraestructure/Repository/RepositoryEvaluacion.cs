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
        public async Task<Evaluacion> Add(int compraId, int idVendedor, int evaluacion1, string comentario,int idCliente)
        {
            int rows1 = 0;
            try
            {
                Evaluacion evaluacion = null;   
                //evaluacion = eva;
                using (MyContext ctx = new MyContext())
                {
                    //// Verificar si la compra y la tienda existen en la base de datos
                    //Compra compra = await ctx.Compra.FindAsync(eva.idCompra);
                    //Tienda tienda = await ctx.Tienda.FindAsync(eva.idVendedor);
                    //Usuario usuario = await ctx.Usuario.FindAsync(eva.idCliente);

                    //if (compra != null && tienda != null)
                    //{
                    //    if (ctx.Entry(compra).State == EntityState.Detached)
                    //        ctx.Compra.Attach(compra);

                    //    if (ctx.Entry(tienda).State == EntityState.Detached)
                    //        ctx.Tienda.Attach(tienda);
                    //    if (ctx.Entry(usuario).State == EntityState.Detached)
                    //        ctx.Usuario.Attach(usuario);
                    //    ctx.Evaluacion.Add(eva);
                    //    rows1= await ctx.SaveChangesAsync();

                    //    //Retorna la evaluación con el ID actualizado


                    //}
                    Usuario usuario = await ctx.Usuario.FindAsync(compraId);
                    Compra compra = await ctx.Compra.FindAsync(compraId);
                    Tienda tienda = await ctx.Tienda.FindAsync(idVendedor);

                    if (compra != null && tienda != null && usuario != null)
                    {
                        if (ctx.Entry(compra).State == EntityState.Detached)
                            ctx.Compra.Attach(compra);

                        if (ctx.Entry(tienda).State == EntityState.Detached)
                            ctx.Tienda.Attach(tienda);

                        if (ctx.Entry(usuario).State == EntityState.Detached)
                            ctx.Usuario.Attach(usuario);

                        Evaluacion eva = new Evaluacion
                        {
                            idCompra = compraId,
                            idVendedor = idVendedor,
                            calificacionACliente = 0,
                            comentarioACliente = "",
                            comentarioAVendedor = comentario,
                            calificacionAVendedor = evaluacion1,
                            Compra = compra,
                            Tienda = tienda,
                            Usuario = usuario,
                            idCliente = usuario.Id,
                            calificacionFinal = 0
                        };

                        ctx.Evaluacion.Add(eva);
                        await ctx.SaveChangesAsync();
                        return eva;
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

        public Evaluacion GetByCompraYvendor(int compraId)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    Evaluacion evaluacion =  ctx.Evaluacion
                        .Where(p => p.idCompra == compraId)
                        .Include(e => e.Compra)
                        .Include(e => e.Usuario)
                        .Include(e => e.Tienda)
                        .FirstOrDefault();

                    return evaluacion;
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
