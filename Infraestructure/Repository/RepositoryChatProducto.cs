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
    public class RepositoryChatProducto : IRepositoryChatProducto
    {
        public async Task<ChatProducto> Crear(ChatProducto chatP, Mensaje mensaje)
        {
            int rows1 = 0;
            try
            {
                ChatProducto chat = null;
                
                if (chatP != null)
                {
                    chat = chatP;
                    // producto.IdProveedor = product.Tienda.Id;

                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        ctx.Mensaje.Attach(mensaje);
                        ctx.ChatProducto.Add(chat);
                        rows1 = await ctx.SaveChangesAsync();
                    }
                }

                return chat;
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

        public ChatProducto GetChatById(int id)
        {
            try
            {
                ChatProducto chat = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    chat = ctx.ChatProducto.Find(id);

                }
                return chat;
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

        public IEnumerable<ChatProducto> GetChatsByProductos(int idProducto)
        {
            try
            {
                IEnumerable<ChatProducto> chats = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    chats = ctx.ChatProducto.Where(U => U.IdProducto == idProducto).Include("Producto").ToList();

                }
                return chats;
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

        public IEnumerable<ChatProducto> GetChatsByTienda(int idTienda)
        {
            try
            {
                IEnumerable<ChatProducto> chats = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    chats = ctx.ChatProducto.Where(U => U.IdTienda == idTienda).Include("Tienda").ToList();

                }
                return chats;
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
