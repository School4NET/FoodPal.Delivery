namespace FoodPal.Contracts
{
    public interface IDeliveriesRequestedEvent
    {
        int Id { get; set; }

        int UserId { get; set; } 
    }
}
