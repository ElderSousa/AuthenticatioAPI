using Domain.MS_AuthorizationAutentication.Entities;
using Domain.MS_AuthorizationAutentication.Interfaces;
using FluentValidation;

namespace Application.MS_AuthenticationAutorization.Validation;

public class UserRoleValidation : AbstractValidator<UserRole>
{
    public UserRoleValidation(IUserRoleRepository userRoleRepository)
    {
        RuleFor(r => r.UserId)
            .NotEmpty()
            .WithMessage(ValidationMessage.requiredField);

        RuleFor(r => r.RoleId)
            .NotEmpty()
            .WithMessage(ValidationMessage.requiredField);

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
            RuleFor(r => r.UserId)
                .MustAsync(userRoleRepository.UserExistAsync)
                .WithMessage(ValidationMessage.NotFound);

            RuleFor(r => r.RoleId)
                .MustAsync(userRoleRepository.UserExistAsync)
                .WithMessage(ValidationMessage.NotFound);

            RuleFor(r => r.ModifiedOn)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);
        });
    }
}
