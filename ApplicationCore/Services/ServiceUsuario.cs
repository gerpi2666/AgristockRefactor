using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuario(string correo, string contrasena)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            string crytpPasswd = Cryptography.EncrypthAES(contrasena);
            return repository.GetUsuario(correo, crytpPasswd);
        }

        public Usuario GetUsuarioById(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioById(id);
            // Desencriptar el password para presentarlo
            oUsuario.Contrasena = Cryptography.DecrypthAES(oUsuario.Contrasena);

            return oUsuario;
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarios();
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para guardarlo
            usuario.Contrasena = Cryptography.EncrypthAES(usuario.Contrasena);
            return repository.Save(usuario);
        }

        public Usuario SaveProveedor(Tienda tienda)
        {
            throw new NotImplementedException();
        }
    }
}
