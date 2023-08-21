using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryEvaluacion
    {
        Task<Evaluacion> GetById(int id);
        Task<IEnumerable<Evaluacion>> GetAll();
       IEnumerable<Evaluacion> GetByClient(int idClient);
        Task<IEnumerable<Evaluacion>> GetBySeller(int idSeller);
        Task<IEnumerable<Tienda>> GetTop5Sellers();
        Task<IEnumerable<Producto>> GetTop5Products();
        Task<Evaluacion> Add(int compraId, int idVendedor, int evaluacion, string comentario, int idCliente);
        Task<Evaluacion> Edit(Evaluacion evaluacion);
        Task<IEnumerable<Tienda>> GetTop3TiendasPeorEvaluadas();
        Evaluacion GetByCompraYvendor(int compraId);



    }
}
