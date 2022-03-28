namespace PlatformService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class Platforms : ControllerBase
{
    #region Constructor
    public Platforms(
         IPlatformRepo platformRepo,
         IMapper mapper,
         ICommandDataClient dataClient)
    {
        PlatformRepo = platformRepo;
        Mapper = mapper;
        DataClient = dataClient;
    }

    private IPlatformRepo PlatformRepo { get; }
    private IMapper Mapper { get; }
    private ICommandDataClient DataClient { get; }
    #endregion

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms....");

        var platformItem = PlatformRepo.GetAllPlatforms();
        return Ok(Mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
    }
    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        var platformItem = PlatformRepo.GetPlatformById(id);
        if (platformItem is null) return NotFound();
        return Ok(Mapper.Map<PlatformReadDto>(platformItem));
    }
    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto dto)
    {
        var platformModel = Mapper.Map<Platform>(dto);
        PlatformRepo.CreatePlatform(platformModel);
        PlatformRepo.SaveChanges();
        var platformReadDto = Mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await DataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"--> Could not send synchronously:{ex.Message}");
        }
        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
}