using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTienda : IRepositoryTienda
    {
        public Tienda GetTiendaById(int id)
        {
            try
            {
                Tienda tienda = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    tienda = ctx.Tienda.Find(id);

                }
                return tienda;
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

        public IEnumerable<Tienda> GetTiendas()
        {
            try
            {
                IEnumerable<Tienda> tiendas = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    tiendas = ctx.Tienda.ToList<Tienda>()
                        ;

                }
                return tiendas;
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

        public Tienda SaveProveedor(Tienda tienda)
        {
            int retorno = 0;
            // objeto que verifica si la tienda ya existe
            Tienda otienda = null;


                using (MyContext ctx = new MyContext())
                { 
                    ctx.Configuration.LazyLoadingEnabled = false;

                    if (otienda == null)
                    {
                        tienda.Activo = true;
                        tienda.Borrado = false;

                        ctx.Tienda.Add(tienda);
                    }
                retorno = ctx.SaveChanges();
            }

            if (retorno >= 0)
                otienda = GetTiendaById(tienda.Id);

            return otienda;


        }
    }
}
