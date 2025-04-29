using RabbitMQ.Client;
using SendMessages.Senders;
using SharedModels.Models;

await RunAsync();

async Task RunAsync()
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var connection = await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();

    var frutaSender = new FrutaSender(channel);
    var usuarioSender = new UsuarioSender(channel);

    string opcao;
    do
    {
        Console.WriteLine("Selecione o tipo de mensagem:");
        Console.WriteLine("1 - Frutas");
        Console.WriteLine("2 - Usuários");
        Console.WriteLine("X - Sair");
        Console.Write("Opção: ");

        opcao = Console.ReadLine()?.ToUpper() ?? string.Empty;

        switch (opcao)
        {
            case "1":
                await EnviarFruta(frutaSender);
                break;

            case "2":
                await EnviarUsuario(usuarioSender);
                break;

            case "X":
                Console.WriteLine("Encerrando...");
                break;

            default:
                Console.WriteLine("Opção inválida!");
                break;
        }

    } while (opcao != "X");
}

async Task EnviarFruta(FrutaSender sender)
{
    var fruta = new Fruta
    {
        Nome = "Manga",
        Descricao = "Fruta tropical rica em vitamina A",
        DataRegistro = DateTime.Now
    };
    await sender.EnviarMensagemAsync(fruta);
}

async Task EnviarUsuario(UsuarioSender sender)
{
    var usuario = new Usuario
    {
        NomeCompleto = "João Silva",
        Endereco = "Rua das Flores, 123",
        Rg = "12.345.678-9",
        Cpf = "123.456.789-00",
        DataRegistro = DateTime.Now
    };
    await sender.EnviarMensagemAsync(usuario);
}
