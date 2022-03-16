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

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Context.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~PlatformRepo()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}