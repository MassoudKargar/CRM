namespace PlatformService.Dtos;
public class PlatformCreateDto
{

    public PlatformCreateDto()
    {
        Name = default!;
        Publisher = default!;
        Cost = default!;
    }
    [Required]
    public string Name { get; set; }

    [Required]
    public string Publisher { get; set; }

    [Required]
    public string Cost { get; set; }
}