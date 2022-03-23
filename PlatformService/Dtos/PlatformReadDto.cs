using System.Globalization;
namespace PlatformService.Dtos;
public class PlatformReadDto
{
    
    public PlatformReadDto()
    {
        Name = default!;
        Publisher = default!;
        Cost = default!;
    }
    public int Id { get; set; }

    public string Name { get; set; }

    public string Publisher { get; set; }

    public string Cost { get; set; }
}