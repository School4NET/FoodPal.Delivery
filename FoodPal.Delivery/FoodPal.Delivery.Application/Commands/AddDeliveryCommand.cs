using FoodPal.Contracts;
using MediatR;

namespace FoodPal.Delivery.Application.Commands
{
    public class AddDeliveryCommand : IRequest<bool>, INewDeliveryRequestedEvent
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
    }
}
