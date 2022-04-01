namespace CommandsService.AsyncDataService;
public class MessageBuaSubscriber : BackgroundService
{

    public MessageBuaSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        Configuration = configuration;
        EventProcessor = eventProcessor;
        InitializeRabbitMQ();
    }

    private IConfiguration Configuration { get; }
    private IEventProcessor EventProcessor { get; }
    private IConnection? Connection { get; set; }
    private IModel? Channel { get; set; }
    private string? QueueName { get; set; }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory()
        {
            HostName = Configuration["RabbitMQHost"],
            Port = int.Parse(Configuration["RabbitMQPort"])
        };
        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();
        Channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        QueueName = Channel.QueueDeclare().QueueName;
        Channel.QueueBind(queue: QueueName, exchange: "trigger", routingKey: "");
        System.Console.WriteLine("--> Listenting on to MessageBus...");

        Connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += (ModuleHandle, ea) =>
        {
            System.Console.WriteLine("--> Event Received!");
            EventProcessor.ProcessEvent(Encoding.UTF8.GetString(ea.Body.ToArray()));
        };
        Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }
    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs? e)
    {
        System.Console.WriteLine("--> RabbitMQ Connection Shutbown");
    }
    public override void Dispose()
    {
        if (Channel?.IsOpen ?? false)
        {
            Channel.Close();
            Connection?.Close();
            base.Dispose();
        }
    }

}