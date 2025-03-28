using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DukandaCore.Application.TourAgencies.Commands.CreateTourAgency;

public record CreateTourAgencyCommand : IRequest<Result<TourAgencyDto>>
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string ContactEmail { get; init; } = null!;
    public string ContactPhone { get; init; } = null!;
    public string Address { get; init; } = null!;
    public IFormFile Logo { get; init; } = null!;
    public int TourAgencyTypeId { get; init; }
}

public class CreateTourAgencyCommandHandler(
    IApplicationDbContext context,
    ICloudinaryService cloudinaryService,
    IUser user)
    : IRequestHandler<CreateTourAgencyCommand, Result<TourAgencyDto>>
{
    public async Task<Result<TourAgencyDto>> Handle(CreateTourAgencyCommand request,
        CancellationToken cancellationToken)
    {
        using var stream = request.Logo.OpenReadStream();
        var logoUrl = await cloudinaryService.UploadFileAsync(stream, request.Logo.FileName);

        var agency = new TourAgency
        {
            Name = request.Name,
            Description = request.Description,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            Address = request.Address,
            LogoUrl = logoUrl,
            TourAgencyTypeId = request.TourAgencyTypeId,
            IsVerified = true,
            UserId = user.Id!.Value,
        };

        context.TourAgencies.Add(agency);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new TourAgencyDto(agency));
    }
}