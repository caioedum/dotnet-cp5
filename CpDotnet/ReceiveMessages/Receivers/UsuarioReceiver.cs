using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveMessages.Receivers
{
    public class UsuarioReceiver
    {
        private readonly IChannel _channel;

        public UsuarioReceiver(IChannel channel)
        {
            _channel = channel;
            _channel.QueueDeclareAsync("usuarios_processados", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public async Task StartConsumingAsync()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Usuário registrado: {message}");

                await Task.Yield();
            };

            await _channel.BasicConsumeAsync("usuarios_processados", autoAck: true, consumer);
        }
    }
}
