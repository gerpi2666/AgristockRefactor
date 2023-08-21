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

        public Tienda GetTiendaById(int id)
        {
            IRepositoryTienda repository = new RepositoryTienda();
            return repository.GetTiendaById(id);
        }

        public void GetTopTresPeoresVendedores(out string etiquetas1, out string valores1)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            repository.GetTopTresPeoresVendedores(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

        public Producto GetProductoMasVendidoVendedor(int idUsuarioVendedor)
        {
            IRepositoryTienda repository = new RepositoryTienda();
            return repository.GetProductoMasVendidoVendedor(idUsuarioVendedor);
        }

        public Usuario GetClienteMasCompras(int idVendedor)
        {
            IRepositoryTienda repository = new RepositoryTienda();
            return repository.GetClienteMasCompras(idVendedor);
        }
    }
}
