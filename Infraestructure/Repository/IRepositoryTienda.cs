﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryTienda
    {
        IEnumerable<Tienda> GetTiendas();
        Tienda GetTiendaById(int id);
        Tienda SaveProveedor(Tienda tienda);
        Tienda GetByVendedor(int vendorID);

        Producto GetProductoMasVendidoVendedor(int idUsuarioVendedor);

        Usuario GetClienteMasCompras(int idVendedor);


    }
}
