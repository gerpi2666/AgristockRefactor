using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryUsuario : IRepositoryUsuario
    {

        public Usuario GetUsuarioById(int id)
        {

            try
            {
                Usuario usuario = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    usuario = ctx.Usuario
                        .Where(p => p.Id == id)
                        .Include(c => c.Perfil)
                        .FirstOrDefault<Usuario>();

                }
                return usuario;
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

        public IEnumerable<Usuario> GetUsuarios()
        {
            try
            {
                IEnumerable<Usuario> Usuarios = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Usuarios = ctx.Usuario.ToList<Usuario>();

                }
                return Usuarios;
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


        public Usuario GetUsuario(string correo, string contrasena)
        {
            Usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.
                     Where(p => p.Correo.Equals(correo) && p.Contrasena == contrasena).
                    FirstOrDefault<Usuario>();
                }
                if (oUsuario != null)
                    oUsuario = GetUsuarioById(oUsuario.Id);
                return oUsuario;
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

        public Usuario Save(Usuario usuario)
        {
            int retorno = 0;
            Usuario oUsuario = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                //oUsuario = GetUsuarioById(usuario.Id);

                IRepositoryPerfil _RepositorioPerfil = new RepositoryPerfil();



                if (oUsuario == null)
                {
                    usuario.Activo = true;
                    usuario.Borrado = false;
                    usuario.Tienda = null;

                    // perfil cliente
                    Perfil perfilToAdd = _RepositorioPerfil.GetPerfilById(2);

                    ctx.Perfil.Attach(perfilToAdd);
                    usuario.Perfil.Add(perfilToAdd);

                    if (usuario.Vendedor)
                    {
                        Perfil perfil3ToAdd = _RepositorioPerfil.GetPerfilById(3);

                        ctx.Perfil.Attach(perfil3ToAdd);
                        usuario.Perfil.Add(perfil3ToAdd);
                    }


                    ctx.Usuario.Add(usuario);

                }
                else
                {
                    ctx.Usuario.Add(usuario);
                    ctx.Entry(usuario).State = EntityState.Modified;
                    retorno += ctx.SaveChanges();
                }

                retorno = ctx.SaveChanges();

            } 

            if (retorno >= 0)
                oUsuario = GetUsuarioById(usuario.Id);

            return oUsuario;

        }
    }
}
