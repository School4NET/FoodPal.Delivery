using FoodPal.Delivery.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FoodPal.Delivery.Processor.ContextFactory
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DeliveryDbContext>
    {
        public DeliveryDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.Development.json")
                    .Build();

            var connectionString = configuration.GetSection("ConnectionStrings:DbConnection").Value;

            return new DeliveryDbContext(connectionString);
        }
    }
}
