namespace CommandsService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class Platforms : ControllerBase
{
    public Platforms()
    {

    }
    [HttpPost]
    public ActionResult TestInboundConnamction()
    {
        Console.WriteLine("--> Inbound POST # Command Service");
        return Ok("Inbound test of platfoems Controller");
    }
}