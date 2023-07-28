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
        public List<Direccion> GetDireccionById(int id)
        {
            IRepositoryDireccion repository = new RepositoryDireccion();
            return repository.GetDireccionById(id);
        }

        public IEnumerable<Direccion> GetDirecciones()
        {
            IRepositoryDireccion repository = new RepositoryDireccion();
            return repository.GetDirecciones();
        }

        public Direccion Save(Direccion direccion, Usuario usuario)
        {
            IRepositoryDireccion repository = new RepositoryDireccion();
            return repository.Save(direccion, usuario);
        }

        //public Provincia Save(Provincia provincia)
        //{
        //    IRepositoryDireccion repository = new RepositoryDireccion();
        //    return repository.SaveProvincia(provincia);
        //}
    }
}
