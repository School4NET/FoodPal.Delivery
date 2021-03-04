using AutoMapper;
using FoodPal.Contracts;
using FoodPal.Delivery.Application.Commands;
using FoodPal.Delivery.Application.Queries;
using FoodPal.Delivery.Common.Exceptions;
using FoodPal.Delivery.Dto.Intern;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Messages
{
    public class DeliveriesRequestedConsumer : IConsumer<IDeliveriesRequestedEvent>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveriesRequestedConsumer> _logger;

        public DeliveriesRequestedConsumer(IServiceScopeFactory serviceScopeFactory, IMapper mapper, ILogger<DeliveriesRequestedConsumer> logger)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IDeliveriesRequestedEvent> context)
        { 
            try
            {
                var command = this._mapper.Map<DeliveriesQuery>(context.Message);

                using (var scope = this._serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var resp = await mediator.Send(command);

                    await context.RespondAsync(resp);
                } 
            }
            catch (ValidationsException e)
            {
                var errors = e.Errors.Aggregate((curr, next) => $"{curr}; {next}");

                var internalErrorResponseDto = new InternalErrorResponseDto
                {
                    Message = "Please correct the validations errors and try again",
                    Details = errors,
                    Type = InternalErrorResponseTypeEnum.Validation
                };
                await context.RespondAsync(internalErrorResponseDto);

                // log ex
                this._logger.LogError(e, errors);
            }
            catch (Exception e)
            {
                var internalErrorResponseDto = new InternalErrorResponseDto
                {
                    Message = $"Something went wrong in {nameof(DeliveriesRequestedConsumer)}",
                    Details = "An error occured",
                    Type = InternalErrorResponseTypeEnum.Error
                };
                await context.RespondAsync(internalErrorResponseDto);

                this._logger.LogError(e, $"Something went wrong in {nameof(DeliveriesRequestedConsumer)}");
            }
        }
    }
}
