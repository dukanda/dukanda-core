using DukandaCore.Domain.Entities;

namespace DukandaCore.Application.News.Dto;

public record NewsGalleryDto
{
    public Guid Id { get; init; }
    public string ImageUrl { get; init; } = null!;
    public string Caption { get; init; } = null!;
    public int DisplayOrder { get; init; }

    public NewsGalleryDto(NewsGallery gallery)
    {
        Id = gallery.Id;
        ImageUrl = gallery.ImageUrl;
        Caption = gallery.Caption;
        DisplayOrder = gallery.DisplayOrder;
    }
} 