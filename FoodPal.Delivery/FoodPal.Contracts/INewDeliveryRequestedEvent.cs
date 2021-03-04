namespace FoodPal.Contracts
{
    public interface INewDeliveryRequestedEvent
    {
        int OrderId { get; set; }

        int UserId { get; set; }
        
        string Author { get; set; }
    }
}
