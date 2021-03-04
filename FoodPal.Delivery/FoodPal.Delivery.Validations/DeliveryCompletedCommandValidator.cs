using FluentValidation;
using FoodPal.Delivery.Application.Commands;

namespace FoodPal.Delivery.Validations
{
    public class DeliveryCompletedCommandValidator : InternalValidator<DeliveryCompletedCommand>
    {
        public DeliveryCompletedCommandValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Author).NotEmpty();
            this.RuleFor(x => x.UserId).NotEmpty(); 
        }
    }
}
