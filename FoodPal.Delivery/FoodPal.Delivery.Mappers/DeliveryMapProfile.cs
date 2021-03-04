using FoodPal.Contracts;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Application.Queries;
using FoodPal.Delivery.Dto;
using System;

namespace FoodPal.Delivery.Mappers
{
    public class DeliveryMapProfile : InternalProfile
    {
        public DeliveryMapProfile()
        {
            this.CreateMap<INewDeliveryRequestedEvent, AddDeliveryCommand>();
            this.CreateMap<AddDeliveryCommand, Domain.Delivery>()
                        .ForMember(x => x.ModifiedBy, opt => opt.MapFrom(z => z.Author))
                        .ForMember(x => x.CreatedBy, opt => opt.MapFrom(z => z.Author))
                        .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(z => DateTimeOffset.Now))
                        .ForMember(x => x.CreatedAt, opt => opt.MapFrom(z => DateTimeOffset.Now)) ;


            this.CreateMap<IDeliveryCompletedRequestedEvent, DeliveryCompletedCommand>();

            this.CreateMap<IDeliveriesRequestedEvent, DeliveriesQuery>();
            this.CreateMap<DeliveryGetDto, Domain.Delivery>().ReverseMap();
        }
    }
}
