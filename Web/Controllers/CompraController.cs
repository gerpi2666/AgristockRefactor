using Infraestructure.Models;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            Compra compra=_ServiceCompra.GetCompraById(id);
            foreach (var item in compra.DetalleCompra)
            {
                subtotal += (double)item.SubTotal*(double) item.Cantidad;
                tiedaNombre = item.Producto.Tienda.NombreProveedor;

            }
            IVA = subtotal*0.13;
            total = IVA + subtotal;
            ViewBag.subtotal = subtotal;
            ViewBag.total = total;
            ViewBag.IVA = IVA; 
            ViewBag.tiendaNombre=tiedaNombre;

            return View(compra);
        }

        // GET: Compra/Create
        public ActionResult CompraCliente()
        {
            IServiceCompra _ServiceCompra = new ServiceCompra();
            IEnumerable<Compra> compra = _ServiceCompra.GetComprasByCliente(4);

            return View(compra);
        }

        public ActionResult CompraTienda()
        {
            IServiceCompra _ServiceCompra = new ServiceCompra();
            IEnumerable<Compra> compra = _ServiceCompra.GetComprasByTienda(1);

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
        public ActionResult Edit(int id)
        {
            return View();
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
