using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryMensaje
    {
        IEnumerable<Mensaje> GetMensajesByChat(int idChat);
        IEnumerable<Mensaje> GetMensajesByRemitente(int idUser);
        Mensaje GetMensajeById(int id);
    }
}
