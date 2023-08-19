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
            //grafico para las compras registradas
            IServiceCompra _ServiceCompra = new ServiceCompra();
            ViewModelGrafico grafico = new ViewModelGrafico();
            _ServiceCompra.GetCompraCountToday(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Ordenes por fecha";
            grafico.tituloEtiquetas = "Cantidad de Ordenes";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "doughnut";
            ViewBag.graficoCountSells = grafico;


            //grafico para las top 5 productos comprados
            IServiceCompra _ServiceCompra2 = new ServiceCompra();
            ViewModelGrafico grafico2 = new ViewModelGrafico();
            _ServiceCompra2.GetTopProductosCompradosMes(out string etiquetas2, out string valores2);
            grafico2.Etiquetas = etiquetas2;
            grafico2.Valores = valores2;
            int cantidadValores2 = valores.Split(',').Length;
            grafico2.Colores = string.Join(",", grafico2.GenerateColors(cantidadValores2));
            grafico2.titulo = "Top 5 productos más vendidos";
            grafico2.tituloEtiquetas = "Top 5 productos más vendidos";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico2.tipo = "doughnut";
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