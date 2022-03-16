namespace PlatformService.Repositories;
public interface IPlatformRepo : IDisposable
{
    bool SaveChanges();
    IEnumerable<Platform> GetAllPlatforms();
    Platform? GetPlatformById(int id);
    void CreatePlatform(Platform platform);
}