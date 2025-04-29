using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveMessages.Receivers
{
    public class FrutaReceiver
    {
        private readonly IChannel _channel;

        public FrutaReceiver(IChannel channel)
        {
            _channel = channel;
            _channel.QueueDeclareAsync("frutas_processadas", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public async Task StartConsumingAsync()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Fruta recebida: {message}");

                await Task.Yield();
            };

            await _channel.BasicConsumeAsync("frutas_processadas", autoAck: true, consumer);
        }
    }
}
