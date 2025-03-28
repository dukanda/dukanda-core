using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Tours.Dto;

namespace DukandaCore.Application.Tours.Queries.CheckTourAvailability;

public record CheckTourAvailabilityQuery : IRequest<Result<TourAvailabilityDto>>
{
    public Guid TourId { get; init; }
    public DateTime Date { get; init; }
    public int RequestedSpots { get; init; }
} 