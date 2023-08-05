using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
  public  interface IServiceMetodoPago
    {
        IEnumerable<MetodoPago> GetMetodosPago();
        List <MetodoPago> GetMetodoPagoById(int id);

        MetodoPago SaveMetodoPago(MetodoPago metodoPago, Usuario usuario);
    }
}
