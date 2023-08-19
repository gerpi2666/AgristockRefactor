using Infraestructure.Models;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
using Web.Utils;
using Web.Security;

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
            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            return View();
        }

        public ActionResult ComprasXCliente()
        {
            Usuario usuario = Session["User"] as Usuario;

            IServiceCompra serviceCompra = new ServiceCompra();
            IEnumerable<Compra> lista = serviceCompra.GetComprasByCliente(usuario.Id);

            return View(lista);
        }

        public ActionResult ComprasXCliente()
        {
            Usuario usuario = Session["User"] as Usuario;

            IServiceCompra serviceCompra = new ServiceCompra();
            IEnumerable<Compra> lista = serviceCompra.GetComprasByCliente(usuario.Id);

            return View(lista);
        }

        [CustomAuthorize((int)Perfil.Cliente, (int)Perfil.Vendedor)]
        public ActionResult ConfirmarCompra(List<Web.ViewModel.ViewModelDetalleCompra> detalleOrden)
        {
            Usuario usuario = Session["User"] as Usuario;
            ViewBag.DetalleOrden = Carrito.Instancia.Items; ;
            IServiceMetodoPago serviceMetodoPago = new ServiceMetodoPago();
            ViewBag.MetodoPago = serviceMetodoPago.GetMetodoPagoById(usuario.Id);
            List<string> direciones = new List<string>();
            foreach(var direccion  in usuario.Direccion)
            {
                direciones.Add(direccion.Provincia + ", " + direccion.Canton + ", " + direccion.Distrito + ", " + direccion.DireccionExacta + ", " + direccion.CodPostal);
            }
            ViewBag.Direccion =direciones;

            return View(Carrito.Instancia.Items);
        }


        [CustomAuthorize((int)Perfil.Vendedor)]
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
        public ActionResult Save(int metodoPago, string observaciones, string direccion)
        {
            Compra compra = new Compra();
            IServiceMetodoPago serviceMetodoPago = new ServiceMetodoPago();
            IServiceProducto serviceProducto = new ServiceProducto();
            Producto producto = null;

            Usuario usuario = Session["User"] as Usuario;
            MetodoPago me = serviceMetodoPago.GetByID(metodoPago);

            compra.FechaHora = Convert.ToDateTime(DateTime.Now);
            compra.IdMetodoPago = me.Id;
            compra.Estado = 1;
            compra.Observaciones = observaciones;
            compra.Direccion = direccion;


                List<DetalleCompra> listaDetalle = new List<DetalleCompra>();
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
                    ordenDetalle.Estado = false;

                producto =serviceProducto.GetProductoById(item.IdProducto);
                    producto.Stock = producto.Stock - item.Cantidad;
                    serviceProducto.Actualizar(producto);

                    listaDetalle.Add(ordenDetalle);

                }
                compra.IdUsuario = usuario.Id;
                compra.Usuario = usuario;
                IServiceCompra serviceCompra = new ServiceCompra();
                serviceCompra.Crear(compra, listaDetalle);

                return RedirectToAction("Index");
           
        }

       

        public ActionResult ordenarProducto(int? idProducto)
        {
            //int cantidadLibros = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idProducto);
            return PartialView("_CompraCantidad");
        }

        public ActionResult actualizarCantidad(int idProd, int Cantidad)
        {
            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idProd, Cantidad);
            TempData.Keep();
            return PartialView("_DetalleCompra", Carrito.Instancia.Items);

        }

        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadLibros = Carrito.Instancia.Items.Count();
            return PartialView("_OrdenCantidad");

        }


        //si le cambio de nombre el ajax da un 404
        public ActionResult eliminarLibro(int? idProducto)
        {
            TempData["NotiCarrito"] = Carrito.Instancia.EliminarItem((int)idProducto);
            TempData.Keep();
            return PartialView("_DetalleCompra", Carrito.Instancia.Items);
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
    
        public ActionResult EvaluationCompraVendedor(int id)
        {
            IServiceTienda serviceTienda = new ServiceTienda();
            Usuario usuario = Session["User"] as Usuario;
            Tienda tienda = serviceTienda.GetByVendedor(usuario.Id);
            ViewBag.idTienda = tienda.Id;
            IServiceCompra serviceCompra = new ServiceCompra();
            Compra compra = serviceCompra.GetCompraById(id);
            return View(compra);
        }

        public ActionResult EvaluationCompraCliente(int id)
        {
            IServiceCompra serviceCompra = new ServiceCompra();
            IServiceTienda serviceTienda = new ServiceTienda();
            Usuario usuario = Session["User"] as Usuario;
            Tienda tienda = serviceTienda.GetByVendedor(usuario.Id);
            ViewBag.idTienda = tienda.Id;
            Compra compra = serviceCompra.GetCompraById(id);
            return View(compra);
        }

        public ActionResult UpdateStateDetail(int compraId, int productoId)
        {
            IServiceCompra serviceCompra = new ServiceCompra();
            

            Compra compra = serviceCompra.GetCompraById(compraId);
            DetalleCompra detalle = new DetalleCompra();
            foreach (var item in compra.DetalleCompra)
            {
                if (item.idProducto == productoId)
                {
                    detalle = item;
                }
            }
            serviceCompra.ChangeStateDetail(compraId,productoId);
            serviceCompra.Actualizar(compra);
            return View();
        }

    }
}
