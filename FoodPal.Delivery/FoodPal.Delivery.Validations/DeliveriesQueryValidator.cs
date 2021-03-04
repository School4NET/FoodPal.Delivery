using FluentValidation;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Application.Queries;

namespace FoodPal.Delivery.Validations
{
    public class DeliveriesQueryValidator : InternalValidator<DeliveriesQuery>
    {
        public DeliveriesQueryValidator()
        {  
            this.RuleFor(x => x.UserId).NotEmpty(); 
        }
    }
}
