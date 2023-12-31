﻿using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCompra : IServiceCompra
    {
        public Compra GetCompraById(int id)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return repository.GetCompraById(id);
        }

        public IEnumerable<Compra> GetCompras()
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return repository.GetCompras();
        }

        public async Task<IEnumerable<Compra>> GetComprasByCliente(int idCliente)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return await repository.GetComprasByCliente(idCliente);
        }

        public async Task<IEnumerable<Compra>> GetComprasByTienda(int idTienda)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return await repository.GetComprasByTienda(idTienda);
        }

        public Task<Compra> Crear(Compra compra, List<DetalleCompra> carrito)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return repository.Crear(compra,carrito);
        }

       public Task<Compra> Actualizar(Compra compra)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return repository.Actualizar(compra);
        }

        public Task Delete(int id)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return repository.Delete(id);
        }

        public int GetCompraCountToday()
        {
            IRepositoryCompra repository = new RepositoryCompra();
            return repository.GetCompraCountToday();
        }

        public async Task ChangeStateDetail(int idCompra, int idProducto)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            await repository.ChangeStateDetail(idCompra,idProducto);
        }       
        public void GetTopProductosCompradosMes(out string etiquetas1, out string valores1)
        {
            IRepositoryCompra repository = new RepositoryCompra();
            repository.GetTopProductosCompradosMes(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

    }
}
