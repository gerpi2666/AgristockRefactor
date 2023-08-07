using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{

    internal partial class ProductoMetadata
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        [Display(Name = "Requerido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Imagen Producto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public byte[] Imagen { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "{0} solo acepta números con dos decimales")]
        public Nullable<double> Precio { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        public Nullable<int> Stock { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Estado { get; set; }

        [Display(Name = "Imagen Producto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Marca { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }

       
    }

    internal partial class TiendaMetadata
    {
        [Display(Name = "Codigo Tienda")]
        public int Id { get; set; }
        public Nullable<int> IdUsuario { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string NombreProveedor { get; set; }
             
        }

    internal partial class DireccionMetadata
    {
        [Display(Name = "Codigo Direccion")]
        public int Id { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
                public string Provincia { get; set; }


        [Display(Name = "Canton")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Canton { get; set; }


        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Distrito { get; set; }

        [Display(Name = "Direccion Exacta")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string DireccionExacta { get; set; }

        [Display(Name = "Codigo Postal")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string CodPostal { get; set; }
              
    }

    internal partial class MetodoPagoMetadata
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> IdUsuario { get; set; }

        [Display(Name = "Tipo de pago")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string TipoPago { get; set; }


        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Proveedor { get; set; }


        [Display(Name = "Número de cuenta")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string NumCuenta { get; set; }


        [Display(Name = "Año de expiración")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> AnioExpiracion { get; set; }


        [Display(Name = "Mes de expiración")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> MesExpiracion { get; set; }
        

       
    }

}
