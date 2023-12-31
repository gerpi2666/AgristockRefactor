﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceTienda
    {
        Tienda SaveProveedor(Tienda tienda);
        Tienda GetByVendedor(int vendorID);
        Tienda GetTiendaById(int id);
        Producto GetProductoMasVendidoVendedor(int idUsuarioVendedor);

        Usuario GetClienteMasCompras(int idVendedor);

    }
}
