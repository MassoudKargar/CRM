namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Platforms : ControllerBase
{
    #region Constructor
    public Platforms(IPlatformRepo platformRepo, IMapper mapper)
    {
        PlatformRepo = platformRepo;
        Mapper = mapper;
    }

    private IPlatformRepo PlatformRepo { get; }
    private IMapper Mapper { get; }
    #endregion

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms....");

        var platformItem = PlatformRepo.GetAllPlatforms();
        return Ok(Mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
    }
    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id){
        var platformItem = PlatformRepo.GetPlatformById(id); 
        if(platformItem is null) return NotFound();
        return Ok(Mapper.Map<PlatformReadDto>(platformItem));
    }
    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto dto){
        var platformModel = Mapper.Map<Platform>(dto);
        PlatformRepo.CreatePlatform(platformModel);
        PlatformRepo.SaveChanges();
        var platformReadDto = Mapper.Map<PlatformReadDto>(platformModel);
        return CreatedAtRoute(nameof(GetPlatformById),new{ Id = platformReadDto.Id},platformReadDto);
    }
}  