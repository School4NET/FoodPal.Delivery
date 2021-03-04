using AutoMapper;
using FluentValidation;
using FoodPal.Contracts;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Application.Extensions;
using FoodPal.Delivery.Data.Abstractions;
using MassTransit;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Application.Handlers
{
    public class DeliveryCompletedCommandHandler : IRequestHandler<DeliveryCompletedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<DeliveryCompletedCommand> _validator;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeliveryCompletedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DeliveryCompletedCommand> validator, IPublishEndpoint _publishEndpoint)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
            this._publishEndpoint = _publishEndpoint;
        }

        public async Task<bool> Handle(DeliveryCompletedCommand request, CancellationToken cancellationToken)
        {
            this._validator.ValidateAndThrowEx(request);

            var repo = this._unitOfWork.GetRepository<Domain.Delivery>();
            var model = repo.Find(x => x.Id == request.Id && x.UserId == request.UserId).First();

            model.Status = Common.Enum.DeliveryStatusEnum.Completed;
            model.ModifiedBy = request.Author;
            model.ModifiedAt = DateTimeOffset.Now;

            repo.Update(model); 

            // save data
            var saved = await this._unitOfWork.SaveChangesAsync();

            await _publishEndpoint.Publish<IDeliveryCompletedEvent>(new
            {
                Title = $"Delivery completed",
                Message = $"Your delivery for order {model.OrderId} is now completed. Thanks you!",
                UserId = request.UserId,
                CreateBy = "system",
                Type = NotificationTypeEnum.InApp,
                Info = string.Empty
            });

            return saved;
        }
    }
}
