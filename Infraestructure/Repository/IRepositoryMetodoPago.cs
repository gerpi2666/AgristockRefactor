using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryMetodoPago
    {
        IEnumerable<MetodoPago> GetMetodosPago();
        List<MetodoPago> GetMetodoPagoById(int id);

        MetodoPago SaveMetodoPago(MetodoPago metodoPago, Usuario usuario);

        MetodoPago GetByID(int id);
    }
}
