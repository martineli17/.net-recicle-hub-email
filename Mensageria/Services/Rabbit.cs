using Crosscuting;
using Mensageria.Contratos;
using Mensageria.Objetos;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Mensageria.Services
{
    public class Rabbit : IRabbit
    {
        private IModel _channel;
        public Rabbit(string connection)
        {
            _channel = CreateConnection(connection);
        }
        public Rabbit()
        {

        }

        public IModel CreateConnection(string connectionString)
        {
            var factory = new ConnectionFactory { Uri = new Uri(connectionString) };
            var connection = factory.CreateConnection();
            return connection.CreateModel();
        }
        public Task<ResponseQueue> CreateQueue(string fila, IModel channel = null)
        {
            var canalEscolhido = channel ?? _channel;
            return Task.FromResult(new ResponseQueue(canalEscolhido.QueueDeclare(queue: fila,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null)));
        }

        public async Task Producer(object request, string fila, IModel channel = null, bool criarQueue = false)
        {
            var canalEscolhido = channel ?? _channel;
            if (criarQueue) await CreateQueue(fila, canalEscolhido);
            canalEscolhido.BasicPublish(string.Empty, fila, null, Encoding.UTF8.GetBytes(JsonFunc.SerializeObject(request)));
        }

        public Task Consumer(Action<ResponseRabbitMQ> action, string fila, IModel channel = null)
        {
            var canalEscolhido = channel ?? _channel;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, events) =>
            {
                action.Invoke(new ResponseRabbitMQ(events));
                canalEscolhido.BasicAck(events.DeliveryTag, false);
            };
            canalEscolhido.BasicConsume(fila, false, consumer);
            return Task.FromResult(0);
        }
    }
}
