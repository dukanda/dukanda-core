using DukandaCore.Application.Common.Interfaces;

public class CreateTourCommandValidator : AbstractValidator<CreateTourCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTourCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título é obrigatório")
            .MaximumLength(200).WithMessage("O título deve ter no máximo 200 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória")
            .MaximumLength(2000).WithMessage("A descrição deve ter no máximo 2000 caracteres");

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("O preço base deve ser maior que 0");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("A data de início é obrigatória")
            .GreaterThan(DateTime.UtcNow).WithMessage("A data de início deve ser no futuro");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("A data de término é obrigatória")
            .GreaterThan(x => x.StartDate).WithMessage("A data de término deve ser posterior à data de início");
        

        RuleFor(x => x.TourTypeIds)
            .NotEmpty().WithMessage("Pelo menos um tipo de tour deve ser selecionado")
            .MustAsync(TourTypesExist).WithMessage("Um ou mais tipos de tour não existem");
    }

    private async Task<bool> AgencyExists(Guid agencyId, CancellationToken cancellationToken)
    {
        return await _context.TourAgencies
            .AnyAsync(a => a.UserId == agencyId, cancellationToken);
    }

    private async Task<bool> TourTypesExist(List<int> tourTypeIds, CancellationToken cancellationToken)
    {
        var existingTypesCount = await _context.TourTypes
            .Where(tt => tourTypeIds.Contains(tt.Id))
            .CountAsync(cancellationToken);

        return existingTypesCount == tourTypeIds.Count;
    }
}