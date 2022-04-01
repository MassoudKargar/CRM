namespace CommandsService.Repositories;
public interface ICommandRepo
{
    #region Platform
    IEnumerable<Platform> GetAllPlatfoem();
    void CreatePlatform(Platform platform);
    bool PlatformExists(int platformId);
    bool ExternalPlatformExists(int externalPlatformId);
    #endregion

    #region Commands
    IEnumerable<Command> GetCommandsForPlatform(int platformId);
    Command? GetCommand(int platformId, int commandId);
    void CreateCommand(int platformId, Command command);
    #endregion
    bool SaveChanges();
}