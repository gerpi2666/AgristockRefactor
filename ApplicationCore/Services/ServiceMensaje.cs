using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMensaje : IServiceMensaje
    {
        public Mensaje GetMensajeById(int id)
        {
            IRepositoryMensaje repository = new RepositoryMensaje();
            return repository.GetMensajeById(id);
        }

        public IEnumerable<Mensaje> GetMensajesByChat(int idChat)
        {
            IRepositoryMensaje repository = new RepositoryMensaje();
            return repository.GetMensajesByChat(idChat);
        }

        public IEnumerable<Mensaje> GetMensajesByRemitente(int idUser)
        {
            IRepositoryMensaje repository = new RepositoryMensaje();
            return repository.GetMensajesByRemitente(idUser);
        }
    }
}
