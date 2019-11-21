using System.Linq;

namespace BookingOffline.Repositories.Interfaces
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity FindById(TKey key);
        TEntity Create(TEntity item);
        bool Delete(TKey key, string userId);
        IQueryable<TEntity> FindAll(string userId);
    }
}
