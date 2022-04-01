namespace CommandsService.Repositories;
public class CommandRepo : ICommandRepo
{
    private AppDbContext Context { get; }

    public CommandRepo(AppDbContext context)
    {
        Context = context;
    }
    #region Platforms
    IEnumerable<Platform> ICommandRepo.GetAllPlatfoem() =>
        Context.platforms.AsEnumerable();
    void ICommandRepo.CreatePlatform(Platform platform)
    {
        if (platform is null) throw new NullReferenceException(nameof(platform));
        Context.platforms.Add(platform);
    }
    bool ICommandRepo.PlatformExists(int platformId) =>
        Context.platforms?.Any(p => p.Id == platformId) ?? false;

    bool ICommandRepo.ExternalPlatformExists(int externalPlatformId) =>
        Context.platforms?.Any(p => p.ExternalID == externalPlatformId) ?? false;
    #endregion
    #region Commands
    void ICommandRepo.CreateCommand(int platformId, Command command)
    {
        if (command is null) throw new NullReferenceException(nameof(command));
        command.PlatformId = platformId;
        Context.Commands.Add(command);
    }


    Command? ICommandRepo.GetCommand(int platformId, int commandId) =>
        Context.Commands
        .FirstOrDefault(c => c.PlatformId == platformId && c.Id == commandId);

    IEnumerable<Command> ICommandRepo.GetCommandsForPlatform(int platformId) =>
        Context.Commands
        .Where(p => p.Id == platformId)
        .OrderBy(c => c.Platform.Name);

    #endregion

    bool ICommandRepo.SaveChanges() =>
        (Context.SaveChanges() >= 0);

}