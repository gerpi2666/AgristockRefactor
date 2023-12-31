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
    
    public partial class Evaluacion
    {
        public int Id { get; set; }
        public Nullable<int> idCompra { get; set; }
        public Nullable<int> idCliente { get; set; }
        public Nullable<int> idVendedor { get; set; }
        public Nullable<int> calificacionACliente { get; set; }
        public string comentarioACliente { get; set; }
        public Nullable<int> calificacionAVendedor { get; set; }
        public string comentarioAVendedor { get; set; }
        public Nullable<int> calificacionFinal { get; set; }
    
        public virtual Compra Compra { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Tienda Tienda { get; set; }
    }
}
