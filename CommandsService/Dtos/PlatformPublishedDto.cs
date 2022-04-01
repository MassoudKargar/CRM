namespace CommandsService.Dtos;
public class PlatformPublishedDto
{
    public PlatformPublishedDto()
    {
        Name = null!;
        Event = null!;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Event { get; set; }
}