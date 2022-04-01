namespace PlatformService.AsyncDataServices;
public class MessageBusClient : IMessageBusClient
{
    public MessageBusClient(IConfiguration configuration)
    {
        Connection = null!;
        Channel = null!;
        Configuration = null!;
        Configuration = configuration;
        var factory = new ConnectionFactory()
        {
            HostName = Configuration["RabbitMQHost"],
            Port = int.Parse(Configuration["RabbitMQPort"])
        };
        try
        {
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            Connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            System.Console.WriteLine("--> Connected MessageBus");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"--> Could not conect to Message Bus: {ex.Message}");
        }
    }

    private IConnection Connection { get; }
    private IModel Channel { get; }
    private IConfiguration Configuration { get; }

    void IMessageBusClient.PublishNewPlatform(PlatformPublishedDto publishedDto)
    {
        var message = JsonSerializer.Serialize(publishedDto);
        if (Connection.IsOpen)
        {
            System.Console.WriteLine("--> RabbitMQ Connection Open, Sending Message...");
            SendMessage(message);
        }
        else
        {
            System.Console.WriteLine("--> RabbitMQ Connection Closed, not Sending");
        }
    }
    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        Channel.BasicPublish(exchange: "trigger",
                    routingKey: "",
                    basicProperties: null,
                    body: body);
        System.Console.WriteLine($"--> We have send {message}");
    }
    public void Dicpose()
    {
        System.Console.WriteLine("--> MessageBus Desposed");
        if (Channel.IsOpen)
        {
            Channel.Close();
            Connection.Close();
        }
    }
    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs? e)
    {
        System.Console.WriteLine("--> RabbitMQ Connection Shutbown");
    }
}