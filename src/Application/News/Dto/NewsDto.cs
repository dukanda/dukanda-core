using DukandaCore.Domain.Entities;

namespace DukandaCore.Application.News.Dto;

public record NewsDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string CoverImageUrl { get; init; } = null!;
    public bool IsFeatured { get; init; }
    public DateTimeOffset? PublishedAt { get; init; }
    public string? PublishedByName { get; init; }
    public int ViewCount { get; init; }

    public NewsDto(Domain.Entities.News news)
    {
        Id = news.Id;
        Title = news.Title;
        Description = news.Description;
        CoverImageUrl = news.CoverImageUrl;
        IsFeatured = news.IsFeatured;
        PublishedAt = news.PublishedAt;
        PublishedByName = news.PublishedBy?.UserName;
        ViewCount = news.ViewCount;
    }
} 