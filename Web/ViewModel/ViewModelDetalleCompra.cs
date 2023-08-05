using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelDetalleCompra
    {
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public virtual Producto Producto { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Precio
        {
            get { return (double)Producto.Precio; }

        }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal
        {
            get
            {
                return calculoSubtotal();
            }
        }

        private double calculoSubtotal()
        {
            return this.Precio * this.Cantidad;
        }


        public ViewModelDetalleCompra(int IdProd)
        {
            IServiceProducto serviceProducto = new ServiceProducto();
            this.IdProducto = IdProd;
            this.Producto = serviceProducto.GetProductoById(IdProd);
        }
    }

}
}