using FoodPal.Delivery.Common.Settings;
using FoodPal.Delivery.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FoodPal.Delivery.Data
{
    public class DeliveryDbContext : DbContext
    {
        private readonly DbSettings _dbSetting;
        public DbSet<User> Users { get; set; }
        public DbSet<Domain.Delivery> Deliveries { get; set; } 

        public DeliveryDbContext(string connectionString)
        {
            this._dbSetting = new DbSettings()
            {
                DbConnection = connectionString
            };
        }

        public DeliveryDbContext(IOptions<DbSettings> dbSetting)
        {
            this._dbSetting = dbSetting.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._dbSetting.DbConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id); 
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.LastName).HasMaxLength(100).IsRequired(); 
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.PhoneNo).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Address).IsRequired();
             
            modelBuilder.Entity<Domain.Delivery>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Delivery>().Property(x => x.Status).IsRequired();
            modelBuilder.Entity<Domain.Delivery>().Property(x => x.OrderId).IsRequired();
            modelBuilder.Entity<Domain.Delivery>().HasOne(x => x.User).WithMany(x => x.Deliveries);
        }
    }
}
