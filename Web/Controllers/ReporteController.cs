using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult graficoCompraCount()
        {
            //Documentación chartjs https://www.chartjs.org/docs/latest/
            IServiceCompra _ServiceOrden = new ServiceCompra();
            ViewModelGrafico grafico = new ViewModelGrafico();
            _ServiceOrden.GetCompraCountToday(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Ordenes por fecha";
            grafico.tituloEtiquetas = "Cantidad de Ordenes";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "doughnut";
            ViewBag.grafico = grafico;
            return View();
        }
    }
}