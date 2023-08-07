//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(MetodoPagoMetadata))]

    public partial class MetodoPago
    {
        public int Id { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string TipoPago { get; set; }
        public string Proveedor { get; set; }
        public string NumCuenta { get; set; }
        public Nullable<int> AnioExpiracion { get; set; }
        public Nullable<int> MesExpiracion { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<bool> Borrado { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
