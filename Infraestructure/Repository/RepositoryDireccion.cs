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
    public class RepositoryDireccion : IRepositoryDireccion
    {
        public List<Direccion> GetDireccionById(int id)
        {
            try
            {
                List <Direccion> direccion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    direccion = ctx.Direccion.Where(D => D.IdUsuario == id).ToList();

                }
                return direccion;
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

        public IEnumerable<Direccion> GetDirecciones()
        {
            try
            {
                IEnumerable<Direccion> direcciones = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    direcciones = ctx.Direccion.ToList<Direccion>();

                }
                return direcciones;
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

        public Direccion Save(Direccion direccion, Usuario usuario)
        {
            try
            {
                int retorno = 0;
                Direccion oDireccion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    ctx.Usuario.Attach(usuario);
                    direccion.Usuario = usuario;
                    ctx.Direccion.Add(direccion);


                    retorno = ctx.SaveChanges();
                }
                return direccion;
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




        //public Provincia GetProvinciaById(int id)
        //{
        //    try
        //    {
        //        Provincia provincia = null;
        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            provincia = ctx.Provincia.Find(id);

        //        }
        //        return provincia;
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw;
        //    }
        //}


        //public Provincia SaveProvincia(Provincia provincia)
        //{
        //    int retorno = 0;  
        //    Provincia oProvincia = null;

        //    using (MyContext ctx = new MyContext())
        //    {
        //        ctx.Configuration.LazyLoadingEnabled = false;
        //        ctx.Provincia.Add(provincia);
        //        retorno = ctx.SaveChanges();
        //    }


        //    if (retorno >= 0)
        //        oProvincia = GetProvinciaById(provincia.Id);

        //    return oProvincia;

        //}

    }
}
