using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using FoodPal.Delivery.Messages;
using MediatR;
using FoodPal.Delivery.Application.Handlers;
using FoodPal.Delivery.Data;
using FoodPal.Delivery.Data.Abstractions;
using FoodPal.Delivery.Mappers;
using FoodPal.Delivery.Common.Settings;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using FoodPal.Delivery.Validations;

namespace FoodPal.Delivery.Processor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((hostBuilder, services) =>
            {
                var messageBrokerSettings = hostBuilder.Configuration.GetSection("MessageBroker").Get<MessageBrokerSettings>();

                // Hosted Service (worker)
                services.AddHostedService<MassTransitConsoleHostedService>();

                services.AddValidatorsFromAssembly(typeof(InternalValidator<>).Assembly);

                // database context
                services.AddScoped<DeliveryDbContext>();

                // database unit of work
                services.Configure<DbSettings>(hostBuilder.Configuration.GetSection("ConnectionStrings"));
                services.AddScoped<IUnitOfWork, UnitOfWork>();

                // register automapper
                services.AddAutoMapper(typeof(InternalProfile).Assembly);

                // mediatR registration
                services.AddMediatR(typeof(AddUserCommandHandler).Assembly);

                // mass transit consumer registration
                services.AddScoped<UserAddedConsumer>();
                services.AddScoped<UserUpdatedConsumer>();

                services.AddScoped<NewDeliveryRequestedConsumer>();
                services.AddScoped<DeliveryCompletedConsumer>();
                services.AddScoped<DeliveriesRequestedConsumer>(); 

                services.AddMassTransit(x => { x.SetKebabCaseEndpointNameFormatter(); x.UsingAzureServiceBus((context, configurator) =>
                {
                    configurator.Host(messageBrokerSettings.ServiceBusHost);

                    configurator.ReceiveEndpoint("user-delivery-queue", configureEndpoint =>
                    {
                        configureEndpoint.Consumer(() => context.GetService<UserAddedConsumer>());
                        configureEndpoint.Consumer(() => context.GetService<UserUpdatedConsumer>());
                    });
                    configurator.ReceiveEndpoint("delivery-queue", configureEndpoint =>
                    {
                        configureEndpoint.Consumer(() => context.GetService<NewDeliveryRequestedConsumer>());
                        configureEndpoint.Consumer(() => context.GetService<DeliveryCompletedConsumer>());
                        configureEndpoint.Consumer(() => context.GetService<DeliveriesRequestedConsumer>());
                    });

                    configurator.ConfigureEndpoints(context);
                });
                });
            });

            await host.RunConsoleAsync();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext hostBuilder, IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(hostBuilder.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddUserSecrets<Program>(); 
        }
    }
}
