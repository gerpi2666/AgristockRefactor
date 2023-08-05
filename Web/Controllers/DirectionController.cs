using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class DirectionController : Controller
    {
        // GET: Direction
        public ActionResult Index()
        {
            return View();
        }


        public List<Direccion> GetDirecciones(int id)
        {
            IServiceDireccion _ServiceDireccion = new ServiceDireccion();
            List<Direccion> direcciones = null;
     
            direcciones =  _ServiceDireccion.GetDireccionById(id); 
            return direcciones;

        }


        [HttpPost]
        public async Task<ActionResult> Save(Direccion direccion)
        {
            IServiceDireccion _ServiceDirection = new ServiceDireccion();
            try
            {

                if (direccion != null)
                {
                    Provincia _Provincia = await BuscarProvinciaAsync(direccion.Provincia);
                    Canton _Canton = await BuscarCantonAsync(direccion.Provincia, direccion.Canton);
                    Distrito _Distrito = await BuscarDistritoAsync(direccion.Provincia, direccion.Canton, direccion.Distrito);
                    Usuario usuario = Session["User"] as Usuario;

                    direccion.Provincia = _Provincia.nombre;
                    direccion.Canton = _Canton.nombre;
                    direccion.Distrito = _Distrito.nombre;

                    //direccion.IdUsuario = Session["User"] is Usuario usuario ? usuario.Id : (int?)null;

                    direccion.Activo = true;
                    direccion.Borrado = false;

                    Direccion direccion1 = _ServiceDirection.Save(direccion, usuario);

                    List<Direccion> direcciones = _ServiceDirection.GetDireccionById(usuario.Id);

                    return Json(new { success = true, direcciones });

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


        private async Task<Provincia> BuscarProvinciaAsync(string provincia)
        {
            string provinciasJsonUrl = "https://ubicaciones.paginasweb.cr/provincias.json";
            Dictionary<int, string> provinciasDictionary;

            using (var httpClient = new HttpClient())
            {
                string json = await httpClient.GetStringAsync(provinciasJsonUrl);
                provinciasDictionary = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);

                foreach (var kvp in provinciasDictionary)
                {
                    if (kvp.Key.ToString() == provincia)
                    {
                        // Encontramos la provincia que coincide, retornamos un objeto Provincia con los datos
                        return new ViewModel.Provincia
                        {
                           id = kvp.Key,
                           nombre = kvp.Value,     
                        };
                    }
                }
            }
            return null;
        }


        private async Task<Canton> BuscarCantonAsync(string provincia, string canton)
        {
            string cantonesJsonUrl = $"https://ubicaciones.paginasweb.cr/provincia/{provincia}/cantones.json";

            Dictionary<int, string> provinciasDictionary;

            using (var httpClient = new HttpClient())
            {
                string json = await httpClient.GetStringAsync(cantonesJsonUrl);
                provinciasDictionary = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);

                foreach (var kvp in provinciasDictionary)
                {
                    if (kvp.Key.ToString() == canton)
                    {
                        // Encontramos la provincia que coincide, retornamos un objeto Provincia con los datos
                        return new Canton
                        {
                            id = kvp.Key,
                            nombre = kvp.Value,
                        };
                    }
                }
            }
            return null;
        }


        private async Task<Distrito> BuscarDistritoAsync(string provincia, string canton, string distrito)
        {
            string distritosJsonUrl = $"https://ubicaciones.paginasweb.cr/provincia/{provincia}/canton/{canton}/distritos.json";

            Dictionary<int, string> provinciasDictionary;

            using (var httpClient = new HttpClient())
            {
                string json = await httpClient.GetStringAsync(distritosJsonUrl);
                provinciasDictionary = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);

                foreach (var kvp in provinciasDictionary)
                {
                    if (kvp.Key.ToString() == distrito)
                    {
                        // Encontramos la provincia que coincide, retornamos un objeto Provincia con los datos
                        return new Distrito
                        {
                            id = kvp.Key,
                            nombre = kvp.Value,
                        };
                    }
                }
            }
            return null;
        }

    }





}