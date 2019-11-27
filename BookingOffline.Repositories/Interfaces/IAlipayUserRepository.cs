using BookingOffline.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BookingOffline.Repositories.Interfaces
{
    public interface IAlipayUserRepository : IRepository<AlipayUser, string>
    {

        IQueryable<AlipayUser> FindAll(params string[] userIds);

        Task UpdateAsync(AlipayUser user);
        AlipayUser FindByAlipayId(string alipayUserId);
    }
}