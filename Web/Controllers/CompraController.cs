using Infraestructure.Models;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {

            IServiceCompra _ServiceCompra = new ServiceCompra();
            ViewBag.compras = _ServiceCompra.GetCompras();
            IEnumerable<Compra> compras = _ServiceCompra.GetCompras();
            return View(compras);
        }

        // GET: Compra/Details/5
        public ActionResult Details(int id)
        {
            double subtotal = 0;
            double total = 0;
            double IVA = 0;
            string tiedaNombre = "";
            IServiceCompra _ServiceCompra = new ServiceCompra();
            Compra compra = _ServiceCompra.GetCompraById(id);
            foreach (var item in compra.DetalleCompra)
            {
                subtotal += (double)item.Producto.Precio * (double)item.Cantidad;
                tiedaNombre = item.Producto.Tienda.NombreProveedor;

            }
            IVA = subtotal * 0.13;
            total = IVA + subtotal;
            ViewBag.subtotal = subtotal;
            ViewBag.total = total;
            ViewBag.IVA = IVA;
            ViewBag.tiendaNombre = tiedaNombre;

            return View(compra);
        }

        // GET: Compra/Create
        public ActionResult CompraCliente()
        {
            IServiceCompra _ServiceCompra = new ServiceCompra();
            IEnumerable<Compra> compra = _ServiceCompra.GetComprasByCliente(5);

            return View(compra);
        }

        public ActionResult CompraTienda()
        {
            Usuario usuario = Session["User"] as Usuario;
            IServiceTienda serviceTienda = new ServiceTienda();
            Tienda tienda = serviceTienda.GetByVendedor(usuario.Id);
            ViewBag.idTienda =tienda.Id;
            IServiceCompra _ServiceCompra = new ServiceCompra();
            IEnumerable<Compra> compra = _ServiceCompra.GetComprasByTienda(tienda.Id);

            return View(compra);
        }

        // POST: Compra/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Compra/Edit/5
        public ActionResult Save(Compra compra)
        {
            IServiceProducto serviceProducto = new ServiceProducto();
            Producto producto = null;
            Usuario usuario = Session["User"] as Usuario;

            if (compra != null)
            {
                List<DetalleCompra> listaDetalle = null;
                var car = Carrito.Instancia.Items;
                foreach (var item in car)
                {
                    DetalleCompra ordenDetalle = new DetalleCompra();
                    ordenDetalle.IdCompra = compra.Id;
                    ordenDetalle.idProducto = item.IdProducto;
                    ordenDetalle.Cantidad = item.Cantidad;
                    ordenDetalle.SubTotal = item.SubTotal;
                    ordenDetalle.Iva = item.SubTotal * 0.13;
                    ordenDetalle.Total = item.SubTotal + (item.SubTotal * 0.13);

                    producto=serviceProducto.GetProductoById(item.IdProducto);
                    producto.Stock = producto.Stock - item.Cantidad;
                    serviceProducto.Actualizar(producto);

                    compra.DetalleCompra.Add(ordenDetalle);
                    listaDetalle.Add(ordenDetalle);

                }
                compra.IdUsuario = usuario.Id;
                compra.Usuario = usuario;
                IServiceCompra serviceCompra = new ServiceCompra();
                serviceCompra.Crear(compra, listaDetalle);

                return RedirectToAction("ProductoAdmin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Compra/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Compra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Compra/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
