using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Tours.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.Tours.Queries.GetPackages;

public record GetPackagesQuery : IRequest<Result<List<PackageDto>>>
{
    public Guid? TourId { get; init; }
}

public class GetPackagesQueryHandler : IRequestHandler<GetPackagesQuery, Result<List<PackageDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPackagesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<PackageDto>>> Handle(GetPackagesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Packages.AsQueryable();

        if (request.TourId.HasValue)
            query = query.Where(p => p.TourId == request.TourId);

        var packages = await query.ToListAsync(cancellationToken);
        
        return Result.Success(_mapper.Map<List<PackageDto>>(packages));
    }
} 