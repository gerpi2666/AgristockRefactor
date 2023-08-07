using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceChatProducto : IServiceChatProducto
    {
        public Task<ChatProducto> Crear(ChatProducto chat, Mensaje mensaje)
        {
            IRepositoryChatProducto repository = new RepositoryChatProducto();
            return repository.Crear(chat, mensaje);
        }

        public ChatProducto GetChatById(int id)
        {
            IRepositoryChatProducto repository = new RepositoryChatProducto();
            return repository.GetChatById(id);
        }

        public IEnumerable<ChatProducto> GetChatsByProductos(int idProducto)
        {
            IRepositoryChatProducto repository = new RepositoryChatProducto();
            return repository.GetChatsByProductos(idProducto);
        }

        public IEnumerable<ChatProducto> GetChatsByTienda(int idTienda)
        {
            IRepositoryChatProducto repository = new RepositoryChatProducto();
            return repository.GetChatsByTienda(idTienda);
        }
    }
}
