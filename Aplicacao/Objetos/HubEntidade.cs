using MongoDB.Bson;

namespace Aplicacao.Objetos
{
    public class HubEntidade
    {
        public ObjectId _id { get; set; }
        public string IdUsuario { get; set; }
        public string IdConexao { get; set; }
        public HubType Tipo { get; set; }
        public HubEntidade()
        {

        }
        public HubEntidade(string idUsuario, string idConexao, HubType tipo)
        {
            IdUsuario = idUsuario;
            IdConexao = idConexao;
            Tipo = tipo;
        }
    }
}
