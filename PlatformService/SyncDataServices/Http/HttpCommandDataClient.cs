namespace PlatformService.SyncDataServices.Http;
public class HttpCommandDataClient : ICommandDataClient
{
    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        HttpClient = httpClient;
        Configuration = configuration;
    }

    private HttpClient HttpClient { get; }
    private IConfiguration Configuration { get; }

    async Task ICommandDataClient.SendPlatformToCommand(PlatformReadDto platformReadDto)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(platformReadDto),
            Encoding.UTF8,
            "application/json"
        );
        var responce = await HttpClient.PostAsync($"{Configuration["CommandService"]}", httpContent);
        if (responce.IsSuccessStatusCode) Console.WriteLine("--> Sync POST to CommandService was OK!");
        else Console.WriteLine("-->Sync POST to CommandService was Not OK!");
    }
}