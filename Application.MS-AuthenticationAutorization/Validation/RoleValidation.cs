using Domain.MS_AuthorizationAutentication.Entities;
using Domain.MS_AuthorizationAutentication.Interfaces;
using FluentValidation;

namespace Application.MS_AuthenticationAutorization.Validation;

public class RoleValidation : AbstractValidator<Role>
{
    public RoleValidation(IRoleRepositoy roleRepositoy)
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage(ValidationMessage.requiredField);

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage(ValidationMessage.requiredField)
            .Length(3, 255)
            .WithMessage(ValidationMessage.MinimumMaximumCharacters);

        When(r => r.ValidationRegister, () =>
        {
            RuleFor(r => r.CreatedOn)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);

            RuleFor(r => r.CreatedOn)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);
        });

        When(r => !r.ValidationRegister, () =>
        {
            RuleFor(r => r.Id)
                .MustAsync(roleRepositoy.UserExistAsync)
                .WithMessage(ValidationMessage.NotFound);

            RuleFor(r => r.ModifiedOn)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);
        });
    }
}