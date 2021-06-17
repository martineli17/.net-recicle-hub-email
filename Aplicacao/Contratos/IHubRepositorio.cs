using Aplicacao.Objetos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aplicacao.Contratos
{
    public interface IHubRepositorio
    {
        Task AddAsync(HubEntidade entidade);
        Task<IReadOnlyList<HubEntidade>> GetAsync(Expression<Func<HubEntidade, bool>> filter);
    }
}
