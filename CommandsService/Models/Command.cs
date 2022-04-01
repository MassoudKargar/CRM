using System.Net.NetworkInformation;
namespace CommandsService.Models;
public class Command
{
    public Command()
    {
        HowTo = null!;
        CommandLine = null!;
        Platform = new();
    }
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string CommandLine { get; set; }
    public int PlatformId { get; set; }
    public Platform Platform { get; set; }
}