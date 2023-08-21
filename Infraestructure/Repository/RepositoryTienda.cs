using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTienda : IRepositoryTienda
    {
        public Tienda GetTiendaById(int id)
        {
            try
            {
                Tienda tienda = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    tienda = ctx.Tienda.Find(id);

                }
                return tienda;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Tienda> GetTiendas()
        {
            try
            {
                IEnumerable<Tienda> tiendas = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    tiendas = ctx.Tienda.ToList<Tienda>()
                        ;

                }
                return tiendas;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Tienda GetByVendedor(int vendorID) {
            try
            {
                Tienda tienda = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    tienda = ctx.Tienda.FirstOrDefault(u => u.IdUsuario == vendorID);


                }
                return tienda;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }


        public Tienda SaveProveedor(Tienda tienda)
        {
            int retorno = 0;
            // objeto que verifica si la tienda ya existe
            Tienda otienda = null;


                using (MyContext ctx = new MyContext())
                { 
                    ctx.Configuration.LazyLoadingEnabled = false;

                    if (otienda == null)
                    {
                        tienda.Activo = true;
                        tienda.Borrado = false;

                        ctx.Tienda.Add(tienda);
                    }
                retorno = ctx.SaveChanges();
            }

            if (retorno >= 0)
                otienda = GetTiendaById(tienda.Id);

            return otienda;


        }


        public Producto GetProductoMasVendidoVendedor(int idUsuarioVendedor)
        {
            Producto productoMasVendido = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var resultado = ctx.Producto
                       .Join(ctx.DetalleCompra, p => p.Id, dc => dc.idProducto, (p, dc) => new { Producto = p, DetalleCompra = dc })
                       .Join(ctx.Compra, pd => pd.DetalleCompra.IdCompra, c => c.Id, (pd, c) => new { ProductoDetalleCompra = pd, Compra = c })
                       .Join(ctx.Tienda, pc => pc.ProductoDetalleCompra.Producto.IdProveedor, t => t.Id, (pc, t) => new { ProductoCompra = pc, Tienda = t })
                       .Join(ctx.Usuario, pt => pt.Tienda.IdUsuario, u => u.Id, (pt, u) => new { ProductoTienda = pt, Usuario = u })
                       .Where(putu => putu.Usuario.Id == idUsuarioVendedor)
                       .GroupBy(putu => new { putu.ProductoTienda.ProductoCompra.ProductoDetalleCompra.Producto.Id, putu.ProductoTienda.ProductoCompra.ProductoDetalleCompra.Producto.Nombre })
                       .Select(grp => new {
                           IdProducto = grp.Key.Id,
                           NombreProducto = grp.Key.Nombre,
                           CantidadTotalVendida = grp.Sum(putu => putu.ProductoTienda.ProductoCompra.ProductoDetalleCompra.DetalleCompra.Cantidad)
                       })
                       .OrderByDescending(item => item.CantidadTotalVendida)
                       .Take(1)
                       .FirstOrDefault();

                    if (resultado != null)
                    {
                        productoMasVendido = new Producto
                        {
                            Id = resultado.IdProducto,
                            Nombre = resultado.NombreProducto,
                        };
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return productoMasVendido;
        }

        public Usuario GetClienteMasCompras(int idVendedor)
        {
            Usuario clienteMasCompras = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    int idClienteMasCompras = ctx.Usuario
                       .Join(ctx.Compra, u => u.Id, c => c.IdUsuario, (u, c) => new { Cliente = u, Compra = c })
                       .Join(ctx.DetalleCompra, cu => cu.Compra.Id, dc => dc.IdCompra, (cu, dc) => new { ClienteCompra = cu, DetalleCompra = dc })
                       .Join(ctx.Producto, cdc => cdc.DetalleCompra.idProducto, p => p.Id, (cdc, p) => new { ClienteDetalleCompra = cdc, Producto = p })
                       .Join(ctx.Tienda, cp => cp.Producto.IdProveedor, t => t.Id, (cp, t) => new { ClienteProducto = cp, Tienda = t })
                       .Join(ctx.Usuario, cpt => cpt.Tienda.IdUsuario, v => v.Id, (cpt, v) => new { ClienteTienda = cpt, Vendedor = v })
                       .Where(cvtv => cvtv.Vendedor.Id == idVendedor)
                       .GroupBy(cvtv => new { cvtv.ClienteTienda.ClienteProducto.ClienteDetalleCompra.ClienteCompra.Cliente.Id, cvtv.ClienteTienda.ClienteProducto.ClienteDetalleCompra.ClienteCompra.Cliente.Nombre })
                       .Select(grp => new {
                           IdCliente = grp.Key.Id,
                           NombreCliente = grp.Key.Nombre,
                           CantidadTotalComprada = grp.Sum(cvtv => cvtv.ClienteTienda.ClienteProducto.ClienteDetalleCompra.DetalleCompra.Cantidad)
                       })
                       .OrderByDescending(item => item.CantidadTotalComprada)
                       .Select(item => item.IdCliente)
                       .FirstOrDefault();

                    if (idClienteMasCompras != 0)
                    {
                        clienteMasCompras = ctx.Usuario.FirstOrDefault(u => u.Id == idClienteMasCompras);
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return clienteMasCompras;
        }




    }
}
