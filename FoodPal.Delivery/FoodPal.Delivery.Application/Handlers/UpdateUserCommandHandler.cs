using AutoMapper;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Domain;
using FoodPal.Delivery.Data.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoodPal.Delivery.Application.Extensions;

namespace FoodPal.Delivery.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateUserCommand> _validator;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateUserCommand> validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            this._validator.ValidateAndThrowEx(request);

            var model = this._mapper.Map<User>(request);
            var repo = this._unitOfWork.GetRepository<User>();
            repo.Update(model);

            // save data
            return await this._unitOfWork.SaveChangesAsync(); 
        }
    }
}
