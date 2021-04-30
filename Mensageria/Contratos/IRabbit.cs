using Mensageria.Objetos;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace Mensageria.Contratos
{
    public interface IRabbit
    {
        IModel CreateConnection(string connectionString);
        Task<ResponseQueue> CreateQueue(string fila, IModel channel = null);
        Task Producer(object request, string fila, IModel channel = null, bool criarQueue = false);
        Task Consumer(Action<ResponseRabbitMQ> action, string fila, IModel channel = null);
    }
}
