using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Security;


namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [CustomAuthorize((int)Perfil.Cliente, (int)Perfil.Vendedor)]
        [HttpGet]
        public async Task<ActionResult> PerfilUsuario()
        {
            ViewBag.ListaProvincias = await ListaProvinciasAsync();          


            Usuario usuario = Session["User"] as Usuario;
            if (usuario != null)
            {
             ViewBag.UsuarioDirecciones = GetDirecciones(usuario.Id);
             ViewBag.MetodosPago = GetMetodoPago(usuario.Id);
            }
            return View();
        }

        [CustomAuthorize((int)Perfil.Administrador)]
        public ActionResult Usuarios()
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario(); 
            IEnumerable<Usuario> lista = _ServiceUsuario.GetUsuarios();
            return View(lista);
        }



        //guardar un usuario
        [HttpPost]
        public ActionResult Save(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {

                if (ModelState.IsValid)
                {

                    Usuario oUsuario = _ServiceUsuario.Save(usuario);

                    Session["User"] = oUsuario;

                    if (usuario.Vendedor == true)
                    {
                        return RedirectToAction("AfiliarseForm", "Login");
                    }
                    else
                    {
                        if (oUsuario != null)
                        {

                            Usuario usuario1 = _ServiceUsuario.GetUsuarioById(oUsuario.Id);
                            return RedirectToAction("Login", "Login", usuario1);
                        }
                    }
                }

                return RedirectToAction("Index", "Home");

            }
            catch (DbEntityValidationException ex)
            {
                // Capturar errores de validación y mostrar detalles
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Entity: {entityValidationErrors.Entry.Entity.GetType().Name}, Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }
                throw;
            }
        }



        [HttpPost]
        public ActionResult Update(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {

                if (ModelState.IsValid)
                {

                    Usuario oUsuario = _ServiceUsuario.Save(usuario);
                   
                }

                return RedirectToAction("Usuarios", "Usuario");

            }
            catch (DbEntityValidationException ex)
            {
                // Capturar errores de validación y mostrar detalles
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Entity: {entityValidationErrors.Entry.Entity.GetType().Name}, Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }
                throw;
            }
        }



        private async Task<List<SelectListItem>> ListaProvinciasAsync()
        {
            string provinciasJsonUrl = "https://ubicaciones.paginasweb.cr/provincias.json";
            Dictionary<int, string> provinciasDictionary;

            using (var httpClient = new HttpClient())
            {
                string json = await httpClient.GetStringAsync(provinciasJsonUrl);
                provinciasDictionary = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);
            }

            // Convertir el diccionario a una lista de SelectListItem
            List<SelectListItem> provinciasList = provinciasDictionary
                .Select(kvp => new SelectListItem
                {
                    Value = kvp.Key.ToString(), // Aquí, el valor será el nombre de la provincia
                    Text = kvp.Value   // Y el texto a mostrar también será el nombre de la provincia
                })
                .ToList();

            return provinciasList;
        }

        public List<Direccion> GetDirecciones(int id)
        {
            IServiceDireccion _ServiceDireccion = new ServiceDireccion();

            List<Direccion> direcciones = _ServiceDireccion.GetDireccionById(id);

            return direcciones;
        }


        public List<MetodoPago> GetMetodoPago(int id)
        {
            IServiceMetodoPago _ServiceMetodoPago = new ServiceMetodoPago();
            List<MetodoPago> metodoPago = _ServiceMetodoPago.GetMetodoPagoById(id);

            return metodoPago;
        }


        public ActionResult Edit(int? id)
        {
            IServiceUsuario _ServiceLibro = new ServiceUsuario ();
            Usuario usuario = null;
            try
            {
                //Si es null el parametro
                if (id == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                usuario = _ServiceLibro.GetUsuarioById(Convert.ToInt32(id));
                if (usuario == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";
                    //Redireccion a la vista del error
                    return RedirectToAction("Default", "Error");

                }
                //ViewBag.IdAutor = ListaAutores(libro.IdAutor);
                //ViewBag.IdCategoria = ListaCategorias(libro.Categoria);
                return View(usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
