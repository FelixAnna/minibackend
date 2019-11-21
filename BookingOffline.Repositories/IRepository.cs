using System.Linq;

namespace BookingOffline.Repositories
{
    public interface IRepository<TModel, TKey>
    {
        TModel FindById(TKey key);
        TModel Create(TModel item);
        bool Delete(TKey key);
        IQueryable FindAll(string userId);
    }
}
