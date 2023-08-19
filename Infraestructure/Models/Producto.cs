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
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.ChatProducto = new HashSet<ChatProducto>();
            this.DetalleCompra = new HashSet<DetalleCompra>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<int> Stock { get; set; }
        public string Estado { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<bool> Borrado { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatProducto> ChatProducto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleCompra> DetalleCompra { get; set; }
        public virtual Tienda Tienda { get; set; }
    }
}
