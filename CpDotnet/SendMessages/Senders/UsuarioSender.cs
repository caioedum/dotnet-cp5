using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMessages.Senders
{
    public class UsuarioSender
    {
        private readonly IChannel _channel;

        public UsuarioSender(IChannel channel)
        {
            _channel = channel;

            _channel.ExchangeDeclareAsync(
                exchange: "topic_usuarios",
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false,
                arguments: null
            ).GetAwaiter().GetResult();
        }

        public async Task EnviarMensagemAsync(Usuario usuario)
        {
            var message = JsonConvert.SerializeObject(usuario);
            var body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(
                exchange: "topic_usuarios",
                routingKey: "usuarios.validacao",
                body: body
            );

            Console.WriteLine($" [x] Usuário enviada: {message}");
        }
    }
}
