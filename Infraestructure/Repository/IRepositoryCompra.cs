﻿using Infraestructure.Models;
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
        IEnumerable<Compra> GetComprasByCliente(int idCliente);
        IEnumerable<Compra> GetComprasByTienda(int idTienda);
        Compra GetCompraById(int id);
    }
}
