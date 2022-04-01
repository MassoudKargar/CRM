namespace CommandsService.Dtos;
public class CommandReadDto
{
    public CommandReadDto()
    {
        HowTo = null!;
        CommandLine = null!;
    }
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string CommandLine { get; set; }
    public int PlatformId { get; set; }
}