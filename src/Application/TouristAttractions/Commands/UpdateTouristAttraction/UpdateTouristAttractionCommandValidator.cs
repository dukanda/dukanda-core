using DukandaCore.Application.Common.Interfaces;

public class UpdateTouristAttractionCommandValidator : AbstractValidator<UpdateTouristAttractionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTouristAttractionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("O ID é obrigatório")
            .MustAsync(AttractionExists).WithMessage("A atração turística não existe");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("O nome da atração é obrigatório")
            .MaximumLength(200).WithMessage("O nome deve ter no máximo 200 caracteres");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória")
            .MaximumLength(2000).WithMessage("A descrição deve ter no máximo 2000 caracteres");

        RuleFor(v => v.Location)
            .NotEmpty().WithMessage("A localização é obrigatória")
            .MaximumLength(500).WithMessage("A localização deve ter no máximo 500 caracteres");

        RuleFor(v => v.CityId)
            .MustAsync(CityExists).WithMessage("A cidade especificada não existe");
    }

    private async Task<bool> AttractionExists(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TouristAttractions.AnyAsync(x => x.Id == id, cancellationToken);
    }

    private async Task<bool> CityExists(int cityId, CancellationToken cancellationToken)
    {
        return await _context.Cities.AnyAsync(c => c.Id == cityId, cancellationToken);
    }
}
