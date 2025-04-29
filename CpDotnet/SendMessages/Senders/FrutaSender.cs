using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedModels.Models;
using System.Text;

namespace SendMessages.Senders
{
    public class FrutaSender
    {
        private readonly IChannel _channel;

        public FrutaSender(IChannel channel)
        {
            _channel = channel;

            _channel.ExchangeDeclareAsync(
                exchange: "topic_frutas",
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false,
                arguments: null
            ).GetAwaiter().GetResult();
        }

        public async Task EnviarMensagemAsync(Fruta fruta)
        {
            var message = JsonConvert.SerializeObject(fruta);
            var body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(
                exchange: "topic_frutas",
                routingKey: "frutas.validacao",
                body: body
            );

            Console.WriteLine($" [x] Fruta enviada: {message}");
        }
    }
}
