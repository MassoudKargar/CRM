
namespace CommandsService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class Platforms : ControllerBase
{
    public Platforms(ICommandRepo commandRepo, IMapper mapper)
    {
        Repository = commandRepo;
        Mapper = mapper;
    }

    private ICommandRepo Repository { get; }
    private IMapper Mapper { get; }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatform()
    {
        Console.WriteLine("--> Getting Platforms from CommandsService");
        return Ok(Mapper.Map<IEnumerable<PlatformReadDto>>(Repository.GetAllPlatfoem()));
    }
    [HttpPost]
    public ActionResult TestInboundConnamction()
    {
        Console.WriteLine("--> Inbound POST # Command Service");
        return Ok("Inbound test of platfoems Controller");
    }
}