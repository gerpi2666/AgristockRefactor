using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
using System;
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
        [HttpGet]
        public async Task<ActionResult> Perfil()
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
    }
}
