using AutoMapper;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Domain;
using FoodPal.Delivery.Data.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoodPal.Delivery.Application.Extensions;
using MassTransit;
using FoodPal.Contracts;

namespace FoodPal.Delivery.Application.Handlers
{
    public class AddDeliveryCommandHandler : IRequestHandler<AddDeliveryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddDeliveryCommand> _validator;
        private readonly IPublishEndpoint _publishEndpoint;

        public AddDeliveryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AddDeliveryCommand> validator, IPublishEndpoint publishEndpoint)
        { 
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
            this._publishEndpoint = publishEndpoint;
        }

        public async Task<bool> Handle(AddDeliveryCommand request, CancellationToken cancellationToken)
        { 
            this._validator.ValidateAndThrowEx(request);

            var model = this._mapper.Map<Domain.Delivery>(request);
            model.Status = Common.Enum.DeliveryStatusEnum.InProgress;

            var repo = this._unitOfWork.GetRepository<Domain.Delivery>();  
            repo.Create(model); 
            var saved = await this._unitOfWork.SaveChangesAsync();

            await _publishEndpoint.Publish<IDeliveryAddedEvent>(new {
                Title = $"Delivery in progress",
                Message = $"Your delivery for order {request.OrderId} is in progress",
                UserId = request.UserId,
                CreateBy = "system",
                Type = NotificationTypeEnum.InApp,
                Info = string.Empty
            });

            return saved;
        }
    }
}
