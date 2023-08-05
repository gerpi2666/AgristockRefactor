using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class MetodoPagoController : Controller
    {
        public async Task<ActionResult> Save(MetodoPago metodoPago)
        {
            IServiceMetodoPago _ServiceMetodoPago = new ServiceMetodoPago();
            try
            {
                if (metodoPago != null)
                {
                    Usuario usuario = Session["User"] as Usuario;

                    MetodoPago oMetodoPago =  _ServiceMetodoPago.SaveMetodoPago(metodoPago, usuario);

                    List<MetodoPago> metodosPago =  _ServiceMetodoPago.GetMetodoPagoById(usuario.Id);

                    return Json(new { success = true, metodosPago });
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
    }
}