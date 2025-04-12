using DukandaCore.Domain.Entities;

namespace DukandaCore.Application.Banners.Dto;

public record BannerDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
    public bool IsFeatured { get; init; }
    public string LinkUrl { get; init; } = null!;
    public int BannerTypeId { get; init; }
    public int DisplayOrder { get; init; }
    public DateTimeOffset Created { get; init; }
    public DateTimeOffset LastModified { get; init; }

    public BannerDto(Banner banner)
    {
        Id = banner.Id;
        Title = banner.Title;
        Description = banner.Description;
        ImageUrl = banner.ImageUrl;
        StartDate = banner.StartDate;
        EndDate = banner.EndDate;
        IsActive = banner.IsActive;
        IsFeatured = banner.IsFeatured;
        LinkUrl = banner.LinkUrl;
        BannerTypeId = banner.BannerTypeId;
        DisplayOrder = banner.DisplayOrder;
        Created = banner.Created;
        LastModified = banner.LastModified;
    }
} 