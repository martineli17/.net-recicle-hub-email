using MongoDB.Driver;

namespace Aplicacao.Repositorio
{
    public class Contexto
    {
        public IMongoDatabase MongoDataBase { get; }
        public string HubCollectionName { get => "HUBEMAIL"; }
        public Contexto(string connectionString, string dataBase)
        {
            var clientMongo = new MongoClient(connectionString);
            MongoDataBase = clientMongo.GetDatabase(dataBase);
        }

        public void AplicarConfiguracoes()
        {
            var collections = MongoDataBase.ListCollectionNames().ToList();
            if (!collections.Contains(HubCollectionName)) MongoDataBase.CreateCollection(HubCollectionName);
        }
    }
}
