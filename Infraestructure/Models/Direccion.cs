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
    
    public partial class Direccion
    {
        public int Id { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string DireccionExacta { get; set; }
        public string CodPostal { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<bool> Borrado { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
