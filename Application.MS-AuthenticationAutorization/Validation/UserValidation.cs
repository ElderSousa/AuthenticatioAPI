using Domain.MS_AuthorizationAutentication.Entities;
using Domain.MS_AuthorizationAutentication.Interfaces;
using FluentValidation;

namespace Application.MS_AuthenticationAutorization.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation(IUserRepository userRepository)
        {
            RuleFor(u => u.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);

            RuleFor(u => u.typeUserRole)
                .IsInEnum()
                .WithMessage(ValidationMessage.requiredField);

            RuleFor(u => u.Active)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);

            When(u => u.ValidationRegister, () =>
            {
                RuleFor(u => u.CreatedOn)
                    .NotEmpty()
                    .WithMessage(ValidationMessage.requiredField);
                
                RuleFor(u => u.CreatedOn)
                    .NotEmpty()
                    .WithMessage(ValidationMessage.requiredField);
            });

            When(u => !u.ValidationRegister, () =>
            {
                RuleFor(u => u.Id)
                    .MustAsync(userRepository.UserExist)
                    .WithMessage(ValidationMessage.NotFound);

                RuleFor(u => u.ModifiedOn)
                    .NotEmpty()
                    .WithMessage(ValidationMessage.requiredField);

                RuleFor(u => u.ModifiedBy)
                    .NotEmpty()
                    .WithMessage(ValidationMessage.requiredField);
            });
        }
    }
}
