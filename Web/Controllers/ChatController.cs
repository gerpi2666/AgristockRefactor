using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ChatController : Controller
    {
        public async Task<ActionResult> Add(int ProductoId, string Pregunta, int userId)
        {
            Usuario usuario = Session["User"] as Usuario;


            IServiceChatProducto serviceChatProducto = new ServiceChatProducto();
            IServiceProducto serviceProducto = new ServiceProducto();

            Producto producto = serviceProducto.GetProductoById(ProductoId);
            ChatProducto chat = new ChatProducto();
            chat.IdProducto = ProductoId;
            chat.IdTienda = producto.Tienda.Id;
            chat.Tienda = producto.Tienda;
            chat.Producto = producto;

            Mensaje mensaje = new Mensaje();
            mensaje.Mensaje1 = Pregunta;
            mensaje.IdRemitente = usuario.Id;
            mensaje.Usuario = usuario;

          await  serviceChatProducto.Crear(chat, mensaje);
            
            


            return View();
        }
    }
}
