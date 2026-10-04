namespace BuildingBlocks.Contracts.Identity;

public class UserRegisteredEvent
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; } = default!;
    
    public string DisplayName { get; set; } = default!;

    // DOB
    public DateTime? DOB { get; set; }
    // Address
    public string Address { get; set; } = default!;
}