using FoodPal.Contracts;
using MediatR;

namespace FoodPal.Delivery.Application.Commands
{
    public class AddUserCommand : IRequest<bool>, IUserAddedEvent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
