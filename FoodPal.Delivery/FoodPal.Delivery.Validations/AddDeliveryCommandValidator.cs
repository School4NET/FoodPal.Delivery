using FluentValidation;
using FoodPal.Delivery.Application.Commands;

namespace FoodPal.Delivery.Validations
{
    public class AddDeliveryCommandValidator : InternalValidator<AddDeliveryCommand>
    {
        public AddDeliveryCommandValidator()
        {
            this.RuleFor(x => x.UserId).NotEmpty();
            this.RuleFor(x => x.Author).NotEmpty();
            this.RuleFor(x => x.OrderId).NotEmpty(); 
        }
    }
}
