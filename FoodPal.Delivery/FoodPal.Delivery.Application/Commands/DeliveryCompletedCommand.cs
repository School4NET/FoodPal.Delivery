using FoodPal.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Application.Commands
{
    public class DeliveryCompletedCommand : IRequest<bool>, IDeliveryCompletedRequestedEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
    }
}
