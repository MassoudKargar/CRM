namespace PlatformService.Repositories;
public class PlatformRepo : IPlatformRepo
{
    private bool disposedValue;

    public PlatformRepo(AppDbContext context)
    {
        Context = context;
    }
    private AppDbContext Context { get; }

    void IPlatformRepo.CreatePlatform(Platform platform)
    {
        if (platform is null) throw new ArgumentNullException();
        Context.Platforms.Add(platform);
    }

    IEnumerable<Platform> IPlatformRepo.GetAllPlatforms() =>
        Context.Platforms.AsEnumerable();

    Platform? IPlatformRepo.GetPlatformById(int id) =>
        Context.Platforms.FirstOrDefault(f => f.Id == id);

    bool IPlatformRepo.SaveChanges() =>
        (Context.SaveChanges() >= 0);
}