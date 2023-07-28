using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }



        // GET: Tienda/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tienda/Create
        public ActionResult Save(Tienda tienda)
        {
            IServiceTienda _ServiceTienda = new ServiceTienda();
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            if (ModelState.IsValid)
            {
                Usuario oUsuario = (Infraestructure.Models.Usuario)Session["User"];
                Usuario usuario1 = _ServiceUsuario.GetUsuarioById(oUsuario.Id);

                tienda.IdUsuario = oUsuario.Id;
                Tienda oTienda = _ServiceTienda.SaveProveedor(tienda);

                
                //session creada para crud de tienda sin pasar por la tabla usuario
                Session["Tienda"] = oTienda;

                return RedirectToAction("Login", "Login", usuario1);

            }
                return RedirectToAction("Index", "Home");

        }

      
        // GET: Tienda/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }




    }
}
