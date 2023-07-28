using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryDireccion
    {
        IEnumerable<Direccion> GetDirecciones();
        List<Direccion> GetDireccionById(int id);

        Direccion Save(Direccion direccion, Usuario usuario);

        //Provincia GetProvinciaById(int id);

        //Provincia SaveProvincia(Provincia provincia);
    }
}
