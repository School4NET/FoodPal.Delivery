using FoodPal.Delivery.Common.Enum;

namespace FoodPal.Delivery.Dto
{
    public class DeliveryGetDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; } 
        public int UserId { get; set; }  
        public DeliveryStatusEnum Status { get; set; }
    }
}
