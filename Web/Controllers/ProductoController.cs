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
using Web.Security;

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

        public ActionResult Edit(int? id)
        {
            try { 
            IServiceProducto serviceProducto = new ServiceProducto();
            Producto producto = null; 
            //Si es null el parametro
            if (id == null)
            {
                return RedirectToAction("IndexAdmin");
            }
            producto = serviceProducto.GetProductoById(Convert.ToInt32(id));
            if (producto == null)
            {
                TempData["Message"] = "No existe el libro solicitado";
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                //Redireccion a la vista del error
                return RedirectToAction("Default", "Error");

            }
                IServiceCategoria _ServiceCategoria = new ServiceCategoria();
                ViewBag.Categorias = ListaCategorias();
                return View(producto);
             }catch (Exception ex)
                {
               
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
                }
           
        }

        [CustomAuthorize((int)Perfil.Vendedor)]
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

        [CustomAuthorize((int)Perfil.Vendedor)]
        public ActionResult ProductoAdminTienda()
        {
            Usuario usuario = Session["User"] as Usuario;
            IServiceTienda serviceTienda = new ServiceTienda();
            Tienda tienda = serviceTienda.GetByVendedor(usuario.Id);

            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> lista = _ServiceProducto.GetProductosByTienda(tienda.Id);

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
        
        public  ActionResult Create(Producto producto)
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            ViewBag.Categorias = ListaCategorias();
            MemoryStream target = new MemoryStream();

          
            


            return View();

        }

        public ActionResult Save(Producto producto, HttpPostedFileBase ImageFile)
        {
            IServiceTienda serviceTienda = new ServiceTienda();
            Usuario usuario = Session["User"] as Usuario;
            Tienda store = serviceTienda.GetByVendedor(usuario.Id);
            producto.IdProveedor = store.Id;
            producto.Tienda = store;
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                // Convierte el archivo a un byte array y asígnalo al modelo Producto
                using (var binaryReader = new BinaryReader(ImageFile.InputStream))
                {
                    producto.Imagen = binaryReader.ReadBytes(ImageFile.ContentLength);
                }
            }

            if (producto != null)
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                _ServiceProducto.Crear(producto,store);

                return RedirectToAction("ProductoAdmin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(Producto producto, HttpPostedFileBase ImageFile)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IServiceTienda serviceTienda = new ServiceTienda();
            Usuario usuario = Session["User"] as Usuario;
            Tienda store = serviceTienda.GetByVendedor(usuario.Id);
            producto.IdProveedor = store.Id;
            Producto p = _ServiceProducto.GetProductoById(producto.Id);
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                // Convierte el archivo a un byte array y asígnalo al modelo Producto
                using (var binaryReader = new BinaryReader(ImageFile.InputStream))
                {
                    producto.Imagen = binaryReader.ReadBytes(ImageFile.ContentLength);
                }
            }
            else
            {
                producto.Imagen = p.Imagen;
            }

            if (producto != null)
            {

               
                producto.Borrado = p.Borrado;
                producto.Activo = p.Activo;
                _ServiceProducto.Actualizar(producto);

                return RedirectToAction("ProductoAdmin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }




        public ActionResult DetalleVendedor()
        {
            Usuario usuario = Session["User"] as Usuario;

            IEnumerable<Producto> productos = null;

            IServiceTienda serviceTienda = new ServiceTienda();
          Tienda tienda=  serviceTienda.GetByVendedor(usuario.Id);

            IServiceProducto _ServiceProducto = new ServiceProducto();
         
            productos = _ServiceProducto.GetProductosByTienda(tienda.Id);
            return View(productos);
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

        public  ActionResult Inventario(int? id,int cantidad)
        {

            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = _ServiceProducto.GetProductoById(Convert.ToInt32(id));
          
           

            if (producto != null)
            {


                producto.Stock = cantidad;
                _ServiceProducto.Actualizar(producto);

                return RedirectToAction("ProductoAdmin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
