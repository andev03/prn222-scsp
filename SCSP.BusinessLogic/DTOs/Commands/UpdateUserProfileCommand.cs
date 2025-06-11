namespace SCSP.BusinessLogic.DTOs.Commands;

public class UpdateUserProfileCommand
{
    public Guid UserGuid { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Bio { get; set; }
} 