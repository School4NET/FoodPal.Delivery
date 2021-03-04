using FoodPal.Delivery.Data.Abstractions;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeliveryDbContext _deliveryDbContext;

        public UnitOfWork(DeliveryDbContext deliveryDbContext)
        {
            this._deliveryDbContext = deliveryDbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this._deliveryDbContext.SaveChangesAsync() > 0;
        }

        IRepository<TEntity> IUnitOfWork.GetRepository<TEntity>()
        {
            return new Repository<TEntity>(this._deliveryDbContext);
        }
    }
}
