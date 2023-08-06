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
    public class RepositoryMetodoPago : IRepositoryMetodoPago
    {
        public List<MetodoPago> GetMetodoPagoById(int id)
        {
            try
            {
                List<MetodoPago> metodoPago = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    metodoPago = ctx.MetodoPago.Where(M => M.IdUsuario == id).ToList();

                }
                return metodoPago;
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

        public IEnumerable<MetodoPago> GetMetodosPago()
        {
            try
            {
                IEnumerable<MetodoPago> metodosPagos = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    metodosPagos = ctx.MetodoPago.ToList<MetodoPago>()
                        ;

                }
                return metodosPagos;
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

        public MetodoPago SaveMetodoPago(MetodoPago metodoPago,Usuario usuario)
        {
            try
            {
                int retorno = 0;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    IRepositoryMetodoPago _RepositorioMetPago = new RepositoryMetodoPago();

                    metodoPago.IdUsuario = usuario.Id;

                    ctx.Usuario.Attach(usuario);
                    ctx.MetodoPago.Add(metodoPago);

                    retorno = ctx.SaveChanges();
                }

                return metodoPago;
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
