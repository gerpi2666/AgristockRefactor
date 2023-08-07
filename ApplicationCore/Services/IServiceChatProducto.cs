using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
  public  interface IServiceChatProducto
    {
        IEnumerable<ChatProducto> GetChatsByProductos(int idProducto);
        IEnumerable<ChatProducto> GetChatsByTienda(int idTienda);
        ChatProducto GetChatById(int id);
        Task<ChatProducto> Crear(ChatProducto chat, Mensaje mensaje);

    }
}
