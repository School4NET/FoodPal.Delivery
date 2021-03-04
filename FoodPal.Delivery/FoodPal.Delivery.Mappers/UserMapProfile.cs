using FoodPal.Contracts;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Domain;

namespace FoodPal.Delivery.Mappers
{
    public class UserMapProfile : InternalProfile
    {
        public UserMapProfile()
        {
            this.CreateMap<IUserAddedEvent, AddUserCommand>();
            this.CreateMap<AddUserCommand, User>();

            this.CreateMap<IUserUpdatedEvent, UpdateUserCommand>();
            this.CreateMap<UpdateUserCommand, User>();
        }
    }
}
