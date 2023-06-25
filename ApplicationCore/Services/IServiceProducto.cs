using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceProducto
    {
        IEnumerable<Producto> GetProductos();
        IEnumerable<Producto> GetProductosByTienda(int id);
        Producto GetProductoById(int id);
    }
}
