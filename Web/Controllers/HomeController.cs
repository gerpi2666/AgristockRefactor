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
            ViewBag.CountSells = _ServiceCompra.GetCompraCountToday();


            //grafico para las top 5 productos comprados
            ViewModelGrafico grafico2 = new ViewModelGrafico();
            _ServiceCompra.GetTopProductosCompradosMes(out string etiquetas2, out string valores2);
            grafico2.Etiquetas = etiquetas2;
            grafico2.Valores = valores2;
            int cantidadValores2 = valores2.Split(',').Length;
            grafico2.Colores = string.Join(",", grafico2.GenerateColors(cantidadValores2));
            grafico2.titulo = "x";
            grafico2.tituloEtiquetas = "Top 5 productos más vendidos";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico2.tipo = "bar";
            ViewBag.graficoTopSells = grafico2;


            //grafico para las top 5 vendedores mejor puntuados
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            ViewModelGrafico grafico3 = new ViewModelGrafico();
            _ServiceUsuario.GetTopCincoVendedores(out string etiquetas3, out string valores3);
            grafico3.Etiquetas = etiquetas3;
            grafico3.Valores = valores3;
            int cantidadValores3 = valores3.Split(',').Length;
            grafico3.Colores = string.Join(",", grafico2.GenerateColors(cantidadValores3));
            grafico3.titulo = "x";
            grafico3.tituloEtiquetas = "Top 5 vendedores mejor puntuados";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico3.tipo = "bar";
            ViewBag.graficoTopSellers = grafico3;



            //grafico para las top 3 vendedores peores puntuados
            ViewModelGrafico grafico4 = new ViewModelGrafico();
            _ServiceUsuario.GetTopTresPeoresVendedores(out string etiquetas4, out string valores4);
            grafico4.Etiquetas = etiquetas4;
            grafico4.Valores = valores4;
            int cantidadValores4 = valores4.Split(',').Length;
            grafico4.Colores = string.Join(",", grafico2.GenerateColors(cantidadValores4));
            grafico4.titulo = "x";
            grafico4.tituloEtiquetas = "Top 3 vendedores peor puntuados";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico4.tipo = "bar";
            ViewBag.graficoWorstSellers = grafico4;


            //producto más vendido del vendedor
            IServiceTienda _ServiceTienda = new ServiceTienda();
            Usuario oUsuario = Session["User"] as Usuario;
            Producto producto = _ServiceTienda.GetProductoMasVendidoVendedor(oUsuario.Id);
            ViewBag.topProducto = producto;

            //Usuario con mas compras hechas a la tienda de la sesion
            Usuario usuario = _ServiceTienda.GetClienteMasCompras(oUsuario.Id);
            ViewBag.topClienteVentas = usuario;


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