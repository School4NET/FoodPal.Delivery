using FoodPal.Contracts;
using FoodPal.Delivery.Dto;
using MediatR;

namespace FoodPal.Delivery.Application.Queries
{
    public class DeliveriesQuery : IRequest<DeliveriesDto>, IDeliveriesRequestedEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
    }
}
