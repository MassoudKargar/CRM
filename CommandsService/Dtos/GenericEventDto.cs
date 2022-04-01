namespace CommandsService.Dtos;
public class GenericEventDto
{
    public GenericEventDto()
    {
        Event = null!;
    }
    public string Event { get; set; }
}