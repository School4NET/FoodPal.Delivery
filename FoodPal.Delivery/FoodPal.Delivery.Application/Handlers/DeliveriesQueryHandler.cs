using AutoMapper;
using FoodPal.Delivery.Application.Queries;
using FoodPal.Delivery.Dto;
using FoodPal.Delivery.Data.Abstractions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoodPal.Delivery.Application.Extensions;

namespace FoodPal.Delivery.Application.Handlers
{
    public class DeliveriesQueryHandler : IRequestHandler<DeliveriesQuery, DeliveriesDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<DeliveriesQuery> _validator;

        public DeliveriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DeliveriesQuery> validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
        }

        public async Task<DeliveriesDto> Handle(DeliveriesQuery request, CancellationToken cancellationToken)
        {
            this._validator.ValidateAndThrowEx(request);

            var repo = this._unitOfWork.GetRepository<Domain.Delivery>();
            var models = repo.Find(x => x.UserId == request.UserId);
            if (request.Id > 0)
            {
                models = models.Where(x => x.Id == request.Id);
            }

            var dtos = this._mapper.Map<List<DeliveryGetDto>>(models.ToList());

            // save data
            return new DeliveriesDto()
            {
                Deliveries = dtos
            };
        }
    }
}
