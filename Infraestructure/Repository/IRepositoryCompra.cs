using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCompra
    {
        IEnumerable<Compra> GetCompras();
        Compra GetCompraById(int id);
    }
}
