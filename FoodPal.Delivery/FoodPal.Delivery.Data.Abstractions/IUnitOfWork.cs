using FoodPal.Delivery.Domain.Abstractions;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Data.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        Task<bool> SaveChangesAsync();
    }
}
