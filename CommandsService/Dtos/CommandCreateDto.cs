namespace CommandsService.Dtos;
public class CommandCreateDto
{
    public CommandCreateDto()
    {
        HowTo = null!;
        CommandLine = null!;
    }
    [Required]
    public string HowTo { get; set; }

    [Required]
    public string CommandLine { get; set; }
}