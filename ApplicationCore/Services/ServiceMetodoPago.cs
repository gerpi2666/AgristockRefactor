using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMetodoPago : IServiceMetodoPago
    {
        public MetodoPago GetMetodoPagoById(int id)
        {
            IRepositoryMetodoPago repository = new RepositoryMetodoPago();
            return repository.GetMetodoPagoById(id);
        }


        public IEnumerable<MetodoPago> GetMetodosPago()
        {
            IRepositoryMetodoPago repository = new RepositoryMetodoPago();
            return repository.GetMetodosPago();
        }
    }
}
