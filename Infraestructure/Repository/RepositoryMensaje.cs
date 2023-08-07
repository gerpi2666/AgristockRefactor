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
    public class RepositoryMensaje : IRepositoryMensaje
    {
        public Mensaje GetMensajeById(int id)
        {
            try
            {
                Mensaje mensaje = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    mensaje = ctx.Mensaje.Find(id);

                }
                return mensaje;
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

        public IEnumerable<Mensaje> GetMensajesByRemitente(int idUser)
        {
            try
            {
                IEnumerable<Mensaje> mensajes = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    mensajes = ctx.Mensaje.Where(U => U.IdRemitente == idUser).Include("Usuario").ToList();

                }
                return mensajes;
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

        public IEnumerable<Mensaje> GetMensajesByChat(int idChat)
        {
            try
            {
                IEnumerable<Mensaje> mensajes = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    mensajes = ctx.Mensaje.Where(m => m.IdChat == idChat).Include("ChatProducto").ToList();
                        
                }
                return mensajes;
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

        public async Task<Mensaje> Crear(Mensaje message, ChatProducto chat, Usuario usuario)
        {
            int rows1 = 0;

            try
            {
                Mensaje mensaje = null;

                if (message != null)
                {
                    mensaje = message;
                    // producto.IdProveedor = product.Tienda.Id;

                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        ctx.ChatProducto.Attach(chat);
                        ctx.Usuario.Attach(usuario);

                        ctx.Mensaje.Add(mensaje);
                        rows1 = await ctx.SaveChangesAsync();
                    }
                }

                return mensaje;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje1 = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje1);
                throw new Exception(mensaje1);
            }
            catch (Exception ex)
            {
                string mensaje1 = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje1);
                throw;
            }
        }
    }
}
