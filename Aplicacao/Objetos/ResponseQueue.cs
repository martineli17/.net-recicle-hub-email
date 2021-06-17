using RabbitMQ.Client;

namespace Aplicacao.Objetos
{
    public class ResponseQueue
    {
        public string Nome { get; private set; }
        public uint QuantidadeConsumidores { get; private set; }
        public uint QuantidadeMensagens { get; private set; }
        public ResponseQueue(QueueDeclareOk queueDeclare)
        {
            Nome = queueDeclare.QueueName;
            QuantidadeConsumidores = queueDeclare.ConsumerCount;
            QuantidadeMensagens = queueDeclare.MessageCount;
        }
        public ResponseQueue()
        {

        }
    }
}
