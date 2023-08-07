using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryChatProducto
    {
        IEnumerable<ChatProducto> GetChatsByProductos(int idProducto);
        IEnumerable<ChatProducto> GetChatsByTienda(int idTienda);
        ChatProducto GetChatById(int id);
        Task<ChatProducto> Crear(ChatProducto chat,Mensaje mensaje);
    }
}
