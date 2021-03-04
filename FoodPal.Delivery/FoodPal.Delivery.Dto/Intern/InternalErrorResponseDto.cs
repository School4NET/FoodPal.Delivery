namespace FoodPal.Delivery.Dto.Intern
{
    public class InternalErrorResponseDto
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public InternalErrorResponseTypeEnum Type { get; set; }
    }

    public enum InternalErrorResponseTypeEnum
    {
        Error,
        Validation
    }
}
