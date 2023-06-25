﻿using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServicePerfil
    {
        IEnumerable<Perfil> GetPerfiles();
        Perfil GetPerfilById(int id);
    }
}
