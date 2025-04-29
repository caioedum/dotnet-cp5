using RabbitMQ.Client;
using ReceiveMessages.Receivers;

await RunAsync();

async Task RunAsync()
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var connection = await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();

    Console.WriteLine("Selecione o receiver (1-Frutas, 2-Usuários):");
    var opcao = Console.ReadLine();

    if (opcao == "1")
    {
        var receiver = new FrutaReceiver(channel);
        await receiver.StartConsumingAsync();
        Console.WriteLine("Receiver 1 (Frutas) aguardando mensagens...");
    }
    else if (opcao == "2")
    {
        var receiver = new UsuarioReceiver(channel);
        await receiver.StartConsumingAsync();
        Console.WriteLine("Receiver 2 (Usuários) aguardando mensagens...");
    }
    else
    {
        Console.WriteLine("Opção inválida!");
        return;
    }

    await Task.Delay(-1); 
}
