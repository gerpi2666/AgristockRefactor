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
        MetodoPago GetMetodoPagoById(int id);
    }
}
