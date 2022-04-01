namespace CommandsService.Controllers;

[Route("api/c/platforms/{platformId}/[controller]")]
[ApiController]
public class Commands : ControllerBase
{
    public Commands(ICommandRepo commandRepo, IMapper mapper)
    {
        Repository = commandRepo;
        Mapper = mapper;
    }

    private ICommandRepo Repository { get; }
    private IMapper Mapper { get; }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
        Console.WriteLine($"--> His GetCommandsForPlatform: {platformId}");
        if (!Repository.PlatformExists(platformId)) return NotFound();
        return Ok(Mapper.Map<CommandReadDto>(Repository.GetCommandsForPlatform(platformId)));
    }
    [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
    {
        Console.WriteLine($"--> His GetCommandForPlatform: {platformId} / {commandId}");
        if (!Repository.PlatformExists(platformId)) return NotFound();
        var command = Repository.GetCommand(platformId, commandId);
        if (command is null) return NotFound();
        return Ok(Mapper.Map<CommandReadDto>(command));
    }
    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
    {
        Console.WriteLine($"--> His CreateCommandForPlatform: {platformId}");
        if (!Repository.PlatformExists(platformId)) return NotFound();
        var command = Mapper.Map<Command>(commandDto);
        Repository.CreateCommand(platformId, command);
        Repository.SaveChanges();
        var commandReadDto = Mapper.Map<CommandReadDto>(command);
        return CreatedAtRoute(nameof(GetCommandForPlatform),
            new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
    }
}