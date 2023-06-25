using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
  public  interface IServiceMensaje
    {
        IEnumerable<Mensaje> GetMensajesByChat(int idChat);
        IEnumerable<Mensaje> GetMensajesByRemitente(int idUser);
        Mensaje GetMensajeById(int id);
    }
}
