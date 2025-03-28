public class AgencyBriefDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string LogoUrl { get; set; } = null!;
    public bool IsVerified { get; set; }
    public string ContactPhone { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
}
