using FoodPal.Delivery.Common.Enum;
using FoodPal.Delivery.Domain.Abstractions;
using System;

namespace FoodPal.Delivery.Domain
{
    public class Delivery : IEntity
    {
        public int Id { get; set; }

        public DeliveryStatusEnum Status { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public int OrderId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }
    }
}
