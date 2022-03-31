namespace PlatformService.Repositories;
public class PlatformRepo : IPlatformRepo
{
    public PlatformRepo(AppDbContext context)
    {
        Context = context;
    }
    private AppDbContext Context { get; } = null!;

    void IPlatformRepo.CreatePlatform(Platform platform)
    {
        if (platform is null) throw new ArgumentNullException();
        Context.Platforms.Add(platform);
    }

    IEnumerable<Platform> IPlatformRepo.GetAllPlatforms() =>
        Context.Platforms.AsEnumerable();

    Platform IPlatformRepo.GetPlatformById(int id) =>
        Context.Platforms.First(f => f.Id == id);

    bool IPlatformRepo.SaveChanges() =>
        (Context.SaveChanges() >= 0);
}