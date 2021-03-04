using FluentValidation;
using FoodPal.Delivery.Application.Commands;

namespace FoodPal.Delivery.Validations
{
    public class UserUpdatedCommandValidator : InternalValidator<UpdateUserCommand>
    {
        public UserUpdatedCommandValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Email).NotEmpty();
            this.RuleFor(x => x.FirstName).NotEmpty();
            this.RuleFor(x => x.LastName).NotEmpty();
            this.RuleFor(x => x.PhoneNo).NotEmpty();
            this.RuleFor(x => x.Address).NotEmpty();
            this.RuleFor(x => x.IsDeleted).NotNull();
        }
    }
}
