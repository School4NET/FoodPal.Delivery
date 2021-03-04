namespace FoodPal.Contracts
{
    public interface IDeliveryCompletedRequestedEvent
    {
        int Id { get; set; }

        int UserId { get; set; }
        
        string Author { get; set; }
    }
}
