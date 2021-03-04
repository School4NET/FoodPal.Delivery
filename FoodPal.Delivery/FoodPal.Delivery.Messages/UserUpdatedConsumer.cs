using AutoMapper;
using FoodPal.Contracts;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Common.Exceptions;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Messages
{
    public class UserUpdatedConsumer : IConsumer<IUserUpdatedEvent>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<UserUpdatedConsumer> _logger;

        public UserUpdatedConsumer(IServiceScopeFactory serviceScopeFactory, IMapper mapper, ILogger<UserUpdatedConsumer> logger)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IUserUpdatedEvent> context)
        { 
            try
            {
                var command = this._mapper.Map<UpdateUserCommand>(context.Message);

                using (var scope = this._serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(command);
                }
            }
            catch (ValidationsException e)
            {
                // TODO: offer validation to end user by persisting it to an audit/log 

                var errors = e.Errors.Aggregate((curr, next) => $"{curr}; {next}");
                this._logger.LogError(e, errors);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $"Something went wrong in {nameof(UserUpdatedConsumer)}");
            }
        }
    }
}
