﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCompra
    {
        IEnumerable<Compra> GetCompras();
        Task<IEnumerable<Compra>> GetComprasByCliente(int idCliente);
        Task<IEnumerable<Compra>> GetComprasByTienda(int idTienda);
        Compra GetCompraById(int id);
        Task<Compra> Crear(Compra compra, List<DetalleCompra> detalle);
        Task<Compra> Actualizar(Compra compra);
        Task Delete(int id);
        Task ChangeStateDetail(int idCompra, int idProducto);
        int GetCompraCountToday();

        void GetTopProductosCompradosMes(out string etiquetas, out string valores);
    }
}
