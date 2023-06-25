using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDireccion : IServiceDireccion
    {
        public Direccion GetDireccionById(int id)
        {
            IRepositoryDireccion repository = new RepositoryDireccion();
            return repository.GetDireccionById(id);
        }

        public IEnumerable<Direccion> GetDirecciones()
        {
            IRepositoryDireccion repository = new RepositoryDireccion();
            return repository.GetDirecciones();
        }
    }
}
