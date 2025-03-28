using FluentValidation;

namespace DukandaCore.Application.Tours.Commands.UpdatePackage;

public class UpdatePackageCommandValidator : AbstractValidator<UpdatePackageCommand>
{
    public UpdatePackageCommandValidator()
    {
        RuleFor(x => x.PackageId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleForEach(x => x.Benefits)
            .ChildRules(benefit =>
            {
                benefit.RuleFor(x => x.Description)
                    .NotEmpty()
                    .MaximumLength(500);
                
                benefit.RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(200);
            });
    }
} 