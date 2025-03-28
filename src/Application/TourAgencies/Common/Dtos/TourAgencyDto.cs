using DukandaCore.Domain.Entities;

public class TourAgencyDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string ContactPhone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string LogoUrl { get; set; } = null!;
    public bool IsVerified { get; set; }
    public int TourAgencyTypeId { get; set; }
    public string AgencyType { get; set; } = null!;
    public int ToursCount { get; set; }

    public TourAgencyDto(TourAgency tourAgency)
    {
        UserId = tourAgency.UserId;
        Name = tourAgency.Name;
        Description = tourAgency.Description;
        ContactEmail = tourAgency.ContactEmail;
        ContactPhone = tourAgency.ContactPhone;
        Address = tourAgency.Address;
        LogoUrl = tourAgency.LogoUrl;
        TourAgencyTypeId = tourAgency.TourAgencyTypeId;
        AgencyType = tourAgency?.TourAgencyType?.Name ?? string.Empty;
        IsVerified = tourAgency?.IsVerified ?? false;
        ToursCount = tourAgency?.Tours?.Count() ?? 0;
    }
}