using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCompra : IServiceCompra
    {
        public Compra GetCompraById(int id)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return repository.GetCompraById(id);
        }

        public IEnumerable<Compra> GetCompras()
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return repository.GetCompras();
        }
    }
}
