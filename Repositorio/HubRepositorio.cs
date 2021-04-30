using Dominio;
using Dominio.Contratos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositorio
{
    public class HubRepositorio : IHubRepositorio
    {
        private readonly IMongoCollection<HubEntidade> _collection;
        private readonly Contexto Context;

        public HubRepositorio(Contexto context)
        {
            Context = context;
            _collection = Context.MongoDataBase.GetCollection<HubEntidade>(Context.HubCollectionName);
        }
        public async Task AddAsync(HubEntidade entidade)
        {
            await _collection.DeleteManyAsync(x => x.IdUsuario == entidade.IdUsuario && x.Tipo == entidade.Tipo);
            await _collection.InsertOneAsync(entidade);
        }

        public async Task<IReadOnlyList<HubEntidade>> GetAsync(Expression<Func<HubEntidade, bool>> filter)
        {
            return (await _collection.FindAsync(filter)).ToList();
        }
    }
}
