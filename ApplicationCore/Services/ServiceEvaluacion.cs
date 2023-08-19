using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEvaluacion : IServiceEvaluacion
    {
        public Task<Evaluacion> Add(Evaluacion evaluacion)
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.Add(evaluacion);
        }

        public Task<Evaluacion> Edit(Evaluacion evaluacion)
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.Edit(evaluacion);
        }

        public Task<IEnumerable<Evaluacion>> GetAll()
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.GetAll();
        }

        public Task<IEnumerable<Evaluacion>> GetByClient(int idClient)
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.GetByClient(idClient);
        }

        public Evaluacion GetByCompraYvendor(int compraId)
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.GetByCompraYvendor(compraId);
        }

        public Task<Evaluacion> GetById(int id)
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.GetById(id);
        }

        public Task<IEnumerable<Evaluacion>> GetBySeller(int idSeller)
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.GetBySeller(idSeller);
        }

        public Task<IEnumerable<Tienda>> GetTop3TiendasPeorEvaluadas()
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.GetTop3TiendasPeorEvaluadas();
        }

        public Task<IEnumerable<Producto>> GetTop5Products()
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.GetTop5Products();
        }

        public Task<IEnumerable<Tienda>> GetTop5Sellers()
        {
            IRepositoryEvaluacion repositoryEvaluacion = new RepositoryEvaluacion();
            return repositoryEvaluacion.GetTop5Sellers();
        }
    }
}
