using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface  IRepositoryProducto
    {
        IEnumerable<Producto> GetProductos();
        IEnumerable<Producto> GetProductosByTienda(int id);
        Producto GetProductoID(int id);
        Task Delete(int id);
        Task<Producto> Crear(Producto producto);
        Task<Producto> Actualizar(Producto producto);
    }
}
