using FoodPal.Contracts;
using FoodPal.Delivery.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly ILogger<DeliveryController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<IDeliveriesRequestedEvent> _requestClient;

        public DeliveryController(ILogger<DeliveryController> logger, IPublishEndpoint publishEndpoint, IRequestClient<IDeliveriesRequestedEvent> requestClient)
        {
            this._logger = logger;
            this._publishEndpoint = publishEndpoint;
            this._requestClient = requestClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int userId, int id)
        {
            var resp = await this._requestClient.GetResponse<DeliveriesDto>(new GetDeliveryDto
            {
                Id = id,
                UserId = userId
            });
            return Ok(resp.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery(DeliveryDto deliveryDto)
        {
            await this._publishEndpoint.Publish<INewDeliveryRequestedEvent>(deliveryDto);

            return Accepted();
        }

        [HttpPatch]
        [Route("Completed")]
        public async Task<IActionResult> MarkAsCompleted(DeliveryCompletedDto deliveryDto)
        {
            await this._publishEndpoint.Publish<IDeliveryCompletedRequestedEvent>(deliveryDto);

            return Accepted();
        }
    }
}
