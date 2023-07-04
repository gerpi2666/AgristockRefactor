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
                    producto = ctx.Producto.Where(p => p.Id == id).Include("Categoria").Include("Tienda").Include(c=>c.ChatProducto).Include(d=>d.ChatProducto.Select(x=>x.Mensaje)).FirstOrDefault();
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

      
    }
}
