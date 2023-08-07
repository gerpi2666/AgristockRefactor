using System;
using ApplicationCore.Services;
using Infraestructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
using System.Data.Entity.Validation;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Index()
        {

            Usuario usuario = new Usuario
            {
                Vendedor = false
            };

            return View(usuario);
            //return View();
        }
        public ActionResult Login(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;

            try
            {
                ModelState.Remove("Nombre");
                ModelState.Remove("Apellidos");
                ModelState.Remove("IdRol");
                //if (ModelState.IsValid)
                //{
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.Correo,usuario.Contrasena);
                    if (oUsuario != null)
                    {
                        Session["User"] = oUsuario;
                        Log.Info($"Accede{oUsuario.Nombre} " +
                            $"con el rol {oUsuario.Perfil}");
                        TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Login",
                            "Usuario autenticado", Util.SweetAlertMessageType.success);

                    foreach (var item in oUsuario.Perfil)
                    {
                        if (item.Id == 1)
                        {
                            return RedirectToAction("DashboardTienda", "Home");
                        }
                    }



                    return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"Intento de inicio de secion{usuario.Correo}");
                        //ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login",
                        //    "Usuario no válido", Util.SweetAlertMessageType.warning);

                        TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Login",
                            "Usuario no válido", Util.SweetAlertMessageType.warning);

                    }
                //}

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

                        
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "No autorizado";
            if (Session["User"] != null)
            {
                Usuario usuario = Session["User"] as Usuario;
                Log.Warn($"No autorizado {usuario.Correo}");
            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                Session.Remove("User");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        [HttpGet]
        public async Task<ActionResult> AfiliarseForm()
        {
            return View();
        }




    }


}
