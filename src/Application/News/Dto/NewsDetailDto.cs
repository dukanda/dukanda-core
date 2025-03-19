using DukandaCore.Domain.Entities;

namespace DukandaCore.Application.News.Dto;

public record NewsDetailDto : NewsDto
{
    public string Content { get; init; } = null!;
    public string? Tags { get; init; }
    public ICollection<NewsGalleryDto> Gallery { get; init; } = new List<NewsGalleryDto>();

    public NewsDetailDto(Domain.Entities.News news) : base(news)
    {
        Content = news.Content;
        Tags = news.Tags;
        Gallery = news.Gallery.Select(g => new NewsGalleryDto(g)).ToList();
    }
} 