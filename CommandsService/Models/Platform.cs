namespace CommandsService.Models;
public class Platform
{
    public Platform()
    {
        Name = null!;
        Commands = new List<Command>();
    }

    public int Id { get; set; }

    public int ExternalID { get; set; }

    public string Name { get; set; }
    public ICollection<Command> Commands { get; set; }
}