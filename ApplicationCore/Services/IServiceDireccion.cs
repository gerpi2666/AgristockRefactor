using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceDireccion
    {
        IEnumerable<Direccion> GetDirecciones();
        List <Direccion> GetDireccionById(int id);

        Direccion Save(Direccion direccion, Usuario usuario);

        //Provincia Save(Provincia provincia);
    }
}
