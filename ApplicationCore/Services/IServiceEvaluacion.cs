using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceEvaluacion
    {
        Task<Evaluacion> GetById(int id);
        Task<IEnumerable<Evaluacion>> GetAll();
        Task<IEnumerable<Evaluacion>> GetByClient(int idClient);
        Task<IEnumerable<Evaluacion>> GetBySeller(int idSeller);
        Task<IEnumerable<Tienda>> GetTop5Sellers();
        Task<IEnumerable<Producto>> GetTop5Products();
        Task<Evaluacion> Add(Evaluacion evaluacion);
        Task<Evaluacion> Edit(Evaluacion evaluacion);
        Task<IEnumerable<Tienda>> GetTop3TiendasPeorEvaluadas();
        Evaluacion GetByCompraYvendor(int compraId);

    }
}
