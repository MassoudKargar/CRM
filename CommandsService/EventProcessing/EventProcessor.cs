namespace CommandsService.EventProcessing;
public class EventProcessor : IEventProcessor
{
    public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Mapper = mapper;
    }

    private IServiceScopeFactory ServiceScopeFactory { get; }
    private IMapper Mapper { get; }

    void IEventProcessor.ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);
        switch (eventType)
        {
            case EventType.PlatformPublished:
                AddPlatform(message);
                break;
            default:
                break;
        }
    }
    private EventType DetermineEvent(string notifcationMessage)
    {
        Console.WriteLine("--> Detrmining Event");
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);
        switch (eventType?.Event)
        {
            case "Platform_Published":
                Console.WriteLine("--> Platform Published Event Detected");
                return EventType.PlatformPublished;
            default:
                Console.WriteLine("--> Could not determine the event type");
                return EventType.Undetermened;
        }
    }
    private void AddPlatform(string platformPublishedMessage)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
            try
            {
                var platform = Mapper.Map<Platform>(platformPublishedDto);
                if (!repo.ExternalPlatformExists(platform.ExternalID))
                {
                    repo.CreatePlatform(platform);
                    repo.SaveChanges();
                }
                else
                {
                    System.Console.WriteLine("--> Platform already exisits....");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not add platform to Db {ex.Message}");
            }
        }

    }
}
enum EventType
{
    PlatformPublished,
    Undetermened
}