using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using SharedModels.Models;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

var frutaConsumer = new AsyncEventingBasicConsumer(channel);
var usuarioConsumer = new AsyncEventingBasicConsumer(channel);

frutaConsumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var fruta = JsonConvert.DeserializeObject<Fruta>(message);

    if (ValidarFruta(fruta))
    {
        await channel.BasicPublishAsync(
            exchange: "topic_frutas",
            routingKey: "frutas.receiver1",
            body: body
        );
        Console.WriteLine($"Fruta validada: {fruta?.Nome}");
    }
};


usuarioConsumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var usuario = JsonConvert.DeserializeObject<Usuario>(message);

    if (ValidarUsuario(usuario))
    {
        await channel.BasicPublishAsync(
            exchange: "topic_frutas",
            routingKey: "frutas.receiver1",
            body: body
        );
        Console.WriteLine($"Usuário validado: {usuario?.NomeCompleto}");
    }
};

await channel.BasicConsumeAsync("frutas_validacao", autoAck: true, frutaConsumer);
await channel.BasicConsumeAsync("usuarios_validacao", autoAck: true, usuarioConsumer);

Console.ReadLine();

bool ValidarFruta(Fruta? fruta)
{
    return !string.IsNullOrEmpty(fruta?.Nome)
        && !string.IsNullOrEmpty(fruta.Descricao)
        && fruta.DataRegistro <= DateTime.Now;
}

bool ValidarUsuario(Usuario? usuario)
{
    return !string.IsNullOrEmpty(usuario?.NomeCompleto)
        && !string.IsNullOrEmpty(usuario.Rg)
        && usuario.DataRegistro <= DateTime.Now;
}