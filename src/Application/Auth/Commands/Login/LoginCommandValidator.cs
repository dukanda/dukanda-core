namespace DukandaCore.Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(v => v.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100);
    }
}
