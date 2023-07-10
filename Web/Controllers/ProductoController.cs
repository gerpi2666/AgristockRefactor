using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            IServiceProducto _ServiceProducto = new ServiceProducto();
            lista = _ServiceProducto.GetProductos();
            return View(lista);
        }

        public ActionResult ProductoTienda()
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> lista = _ServiceProducto.GetProductosByTienda(2);
            return View(lista);
        }

        public ActionResult ProductoAdmin()
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> lista = _ServiceProducto.GetProductos();
            return View(lista);
        }
         private Usuario getUser(int id)
        {
            IServiceUsuario serviceUsuario = new ServiceUsuario();
            return serviceUsuario.GetUsuarioById(id);
        }
        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = null;
            if (id == null)
            {
                return RedirectToAction("Index");

            }
            producto = _ServiceProducto.GetProductoById(id);
            return View(producto);
        }

        // GET: Producto/Create
       
        
        private SelectList ListaCategorias(int id=0)
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> lista = _ServiceCategoria.GetCategorias();
            return new SelectList(lista,"Id","Nombre",id);
        }
        
        public async Task<ActionResult> Create(Producto producto, HttpPostedFileBase imagen)
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            ViewBag.Categorias = ListaCategorias();
            MemoryStream target = new MemoryStream();

            if (imagen != null)
            {
                imagen.InputStream.CopyTo(target);
                producto.Imagen = target.ToArray();
                ModelState.Remove("Imagen");
                producto.IdProveedor = 2;
                producto.IdCategoria = 1;
            }
           

            if(ModelState.IsValid)
                {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                await _ServiceProducto.Crear(producto);
            }


            
            return View();

        }

        public ActionResult DetalleVendedor()
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = null;
            //if (id == null)
            //{
            //    return RedirectToAction("Index");

            //}
            producto = _ServiceProducto.GetProductoById(1);
            return View(producto);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IServiceProducto _ServiceProducto = new ServiceProducto();
            await _ServiceProducto.Delete(id.Value);

            return RedirectToAction("Index");
        }

    }
}
