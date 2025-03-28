using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Tours.Dtos;
using DukandaCore.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.Tours.Commands.CreatePackage;

public record CreatePackageCommand : IRequest<Result<PackageDto>>
{
    public Guid TourId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int MaxParticipants { get; init; }
}

public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, Result<PackageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreatePackageCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PackageDto>> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        var package = new Package
        {
            TourId = request.TourId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            MaxParticipants = request.MaxParticipants
        };

        _context.Packages.Add(package);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new PackageDto(package));
    }
} 