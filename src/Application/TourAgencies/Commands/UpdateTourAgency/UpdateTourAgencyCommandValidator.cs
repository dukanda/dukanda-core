using DukandaCore.Application.Common.Interfaces;

public class UpdateTourAgencyCommandValidator : AbstractValidator<UpdateTourAgencyCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTourAgencyCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(200).WithMessage("O nome deve ter no máximo 200 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória")
            .MaximumLength(2000).WithMessage("A descrição deve ter no máximo 2000 caracteres");

        RuleFor(x => x.ContactEmail)
            .NotEmpty().WithMessage("O email de contato é obrigatório")
            .EmailAddress().WithMessage("Formato de email inválido");

        RuleFor(x => x.ContactPhone)
            .NotEmpty().WithMessage("O telefone de contato é obrigatório")
            .Matches(@"^\+?[\d\s-]+$").WithMessage("Formato de telefone inválido");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("O endereço é obrigatório")
            .MaximumLength(500).WithMessage("O endereço deve ter no máximo 500 caracteres");

        RuleFor(x => x.TourAgencyTypeId)
            .MustAsync(AgencyTypeExists).WithMessage("O tipo de agência não existe");
    }

    private async Task<bool> AgencyTypeExists(int TourAgencyTypeId, CancellationToken cancellationToken)
    {
        return await _context.AgencyTypes
            .AnyAsync(a => a.Id == TourAgencyTypeId, cancellationToken);
    }
}