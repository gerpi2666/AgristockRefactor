using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class EvaluacionController : Controller
    {
        // GET: Evaluacion
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(int compraId, int idVendedor,int evaluacion, string comentario)
        {
            IServiceCompra serviceCompra = new ServiceCompra();
            IServiceTienda serviceTienda = new ServiceTienda();
            IServiceEvaluacion serviceEvaluacion = new ServiceEvaluacion();

            Usuario usuario = Session["User"] as Usuario;


            Tienda tienda = serviceTienda.GetByVendedor(idVendedor);
            Compra compra = serviceCompra.GetCompraById(compraId);

            Evaluacion eva = new Evaluacion();
            eva.idCompra=compraId;
            eva.idVendedor = idVendedor;
            eva.calificacionACliente = evaluacion;
            eva.comentarioACliente = comentario;
            eva.comentarioAVendedor = "";
            eva.calificacionAVendedor = 0;
            eva.Compra = compra;
            eva.Tienda = tienda;
            eva.Usuario = usuario;//cliente

            await serviceEvaluacion.Add(eva);

            return View();
        }
    }
}