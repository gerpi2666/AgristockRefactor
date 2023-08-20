using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCompra
    {
        IEnumerable<Compra> GetCompras();
        Task<IEnumerable<Compra>> GetComprasByCliente(int idCliente);
       Task< IEnumerable<Compra>> GetComprasByTienda(int idTienda);
        Compra GetCompraById(int id);
        Task<Compra> Crear(Compra compra, List<DetalleCompra> carrito);
        Task<Compra> Actualizar(Compra compra);
        Task Delete(int id);

        Task ChangeStateDetail(int idCompra, int idProducto);
        void GetTopProductosCompradosMes(out string etiquetas, out string valores);

        int GetCompraCountToday();

    }
}
