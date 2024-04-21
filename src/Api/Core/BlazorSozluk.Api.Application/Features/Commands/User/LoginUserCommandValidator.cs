using BlazorSozluk.Common.Models.RequestModels;
using FluentValidation;

namespace BlazorSozluk.Api.Application.Features.Commands.User;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(i => i.EmailAddress)
            .NotNull()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{PropertyName} not a valid email adress");


        RuleFor(i => i.Password)
           .NotNull()
           .MinimumLength(6).WithMessage("{PropertyName} should at least be (MinLength) chracters");
    }
}
