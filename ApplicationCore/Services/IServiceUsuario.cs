using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
  public  interface IServiceUsuario
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioById(int id);

        Usuario GetUsuario(string correo, string contrasena);

        Usuario Save(Usuario usuario);


        void GetTopCincoVendedores(out string etiquetas, out string valores);
        void GetTopTresPeoresVendedores(out string etiquetas, out string valores);



    }
}
