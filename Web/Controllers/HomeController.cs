using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Security;
using Web.ViewModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.NotificationMessage = TempData["mensaje"];
            }
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> lista = _ServiceProducto.GetProductos();
            return View(lista);
        }


        [CustomAuthorize((int)Perfil.Administrador, (int)Perfil.Vendedor)]
        public ActionResult DashboardTienda()
        {
            //compras registradas
            IServiceCompra _ServiceCompra = new ServiceCompra();
            ViewBag.graficoCountSells = _ServiceCompra.GetCompraCountToday();


            //grafico para las top 5 productos comprados
            IServiceCompra _ServiceCompra2 = new ServiceCompra();
            ViewModelGrafico grafico2 = new ViewModelGrafico();
            _ServiceCompra2.GetTopProductosCompradosMes(out string etiquetas2, out string valores2);
            grafico2.Etiquetas = etiquetas2;
            grafico2.Valores = valores2;
            int cantidadValores2 = valores2.Split(',').Length;
            grafico2.Colores = string.Join(",", grafico2.GenerateColors(cantidadValores2));
            grafico2.titulo = "x";
            grafico2.tituloEtiquetas = "Top 5 productos más vendidos";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico2.tipo = "bar";
            ViewBag.graficoTopSells = grafico2;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}