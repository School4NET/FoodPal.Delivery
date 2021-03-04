using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Processor
{
    public class MassTransitConsoleHostedService : IHostedService
    {
        private readonly IBusControl _bus;

        public MassTransitConsoleHostedService(IBusControl bus)
        {
            this._bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this._bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this._bus.StopAsync(cancellationToken);
        }
    }
}
