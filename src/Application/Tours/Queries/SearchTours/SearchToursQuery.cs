using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Tours.Queries.SearchTours;

public record SearchToursQuery : IRequest<Result<List<TourDto>>>
{
    public string? Search { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public Guid? CityId { get; init; }
    public Guid? TourTypeId { get; init; }
} 