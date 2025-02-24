using DukandaCore.Application.Common.Interfaces;

public class CreateTouristAttractionCommandValidator : AbstractValidator<CreateTouristAttractionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTouristAttractionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

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

        RuleFor(v => v.Image)
            .NotEmpty().WithMessage("A imagem é obrigatória");
    }

    private async Task<bool> CityExists(int cityId, CancellationToken cancellationToken)
    {
        return await _context.Cities.AnyAsync(c => c.Id == cityId, cancellationToken);
    }
}
