using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Web.Security
{
    public class AutorizeView
    {
        public static bool IsUserInRole(string[] nombreRoles)
        {
            //castea cada iteracion
            IEnumerable<Perfil> allowedroles = nombreRoles.
                Select(a => (Perfil)Enum.Parse(typeof(Perfil), a));
            bool authorize = false;
            var oUsuario = (Usuario)HttpContext.Current.Session["User"];
            if (oUsuario != null)
            {
                foreach (var rol in allowedroles)
                {
                    if ((int)rol == oUsuario.Id)
                        return true;
                }
            }
            return authorize;
        }
    }
}