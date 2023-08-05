using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTienda : IServiceTienda
    {
        public Tienda SaveProveedor(Tienda tienda)
        {
            IRepositoryTienda repository = new RepositoryTienda();
            return repository.SaveProveedor(tienda);
        }

        public Tienda GetByVendedor(int vendorID)
        {
            IRepositoryTienda repository = new RepositoryTienda();
            return repository.GetByVendedor(vendorID);
        }

    }
}
