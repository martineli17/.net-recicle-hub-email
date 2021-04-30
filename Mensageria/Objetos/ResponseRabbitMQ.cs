using Crosscuting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Mensageria.Objetos
{
    public class ResponseRabbitMQ
    {
        public IBasicProperties BasicProperties { get; private set; }
        public ReadOnlyMemory<byte> Body { get; private set; }
        public string ConsumerTag { get; private set; }
        public ulong DeliveryTag { get; private set; }
        public string Exchange { get; private set; }
        public bool Redelivered { get; private set; }
        public string RoutingKey { get; private set; }
        public ResponseRabbitMQ(BasicDeliverEventArgs eventArgs)
        {
            BasicProperties = eventArgs.BasicProperties;
            Body = eventArgs.Body;
            ConsumerTag = eventArgs.ConsumerTag;
            DeliveryTag = eventArgs.DeliveryTag;
            Exchange = eventArgs.Exchange;
            Redelivered = eventArgs.Redelivered;
            RoutingKey = eventArgs.RoutingKey;
        }

        public TReturn GetBody<TReturn>() => JsonFunc.DeserializeObject<TReturn>(Encoding.UTF8.GetString(Body.ToArray()));
    }
}
