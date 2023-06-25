using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePerfil : IServicePerfil
    {
        public Perfil GetPerfilById(int id)
        {
            IRepositoryPerfil repository = new RepositoryPerfil();
            return repository.GetPerfilById(id);
        }

        public IEnumerable<Perfil> GetPerfiles()
        {
            IRepositoryPerfil repository = new RepositoryPerfil();
            return repository.GetPerfiles();
        }
    }
}
