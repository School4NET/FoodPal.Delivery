using FoodPal.Delivery.Data.Abstractions;
using FoodPal.Delivery.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DeliveryDbContext dbContext)
        {
            this._dbSet = dbContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            this._dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            this._dbSet.Remove(entity);
        }

        public async Task DeleteAsync(int entityId)
        { 
            this._dbSet.Remove(await this.FindByIdAsync(entityId));
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> searchExpression)
        {
            return this._dbSet.Where(searchExpression);
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await this._dbSet.FirstAsync(x => x.Id == id);
        }

        public void Update(TEntity entity)
        {
            this._dbSet.Update(entity);
        }
    }
}
