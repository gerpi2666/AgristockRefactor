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
    public class RepositoryProducto : IRepositoryProducto
    {
      

        public Producto GetProductoID(int id)
        {
            try
            {
                Producto producto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    producto = ctx.Producto.Find(id);

                    producto = ctx.Producto.Where(p => p.Id == id).Include("Categoria")
                        .Include("Tienda").Include(c=>c.ChatProducto)
                        .Include(p => p.ChatProducto.Select(cp => cp.Mensaje.Select(m => m.Usuario)))
                        .Include(d=>d.ChatProducto.Select(x=>x.Mensaje)).FirstOrDefault();
                }
                return producto;
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


        //Productos que los clientes ven al momento de la compra
        public IEnumerable<Producto> GetProductos()
        {
            try
            {

                IEnumerable<Producto> productos = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    productos = ctx.Producto.ToList<Producto>();

                }
                return productos;

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


        //Productos asociados a una tienda
        public IEnumerable<Producto> GetProductosByTienda(int id)
        {
            try
            {
                IEnumerable<Producto> productos = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    productos = ctx.Producto.Where(p => p.IdProveedor==id).ToList<Producto>();

                }
                    return productos;
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
                Producto producto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    producto = await ctx.Producto.FindAsync(id);
                    if (producto != null)
                    {
                        producto.Borrado = true;
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

        public async Task<Producto> Crear(Producto product, Tienda store)
        {
            int rows1 = 0;
            try
            {
                Producto producto = null;
                if (product != null)
                {
                   // producto.IdProveedor = product.Tienda.Id;
                    producto = product;
                    producto.Activo = true;
                    producto.Borrado = false;
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        ctx.Tienda.Attach(store);
                        ctx.Producto.Add(producto);
                        rows1 = await ctx.SaveChangesAsync();
                    }
                }
               
                return producto;
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

        public async Task<Producto> Actualizar(Producto producto)
        {
            int rows1 = 0;
            try
            {
                
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                  
                    if (producto != null)
                    {
                        ctx.Entry(producto).State = EntityState.Modified;
                        rows1 = await ctx.SaveChangesAsync();
                    }
                }
                return producto;

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
