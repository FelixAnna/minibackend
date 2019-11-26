using BookingOffline.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BookingOffline.Repositories.Interfaces
{
    public interface IAlipayUserRepository : IRepository<AlipayUser, string>
    {

        IQueryable<AlipayUser> FindAll(params string[] userIds);

        Task UpdateAsync(string userId, string nickName, string photo);
        AlipayUser FindByAlipayId(string alipayUserId);
    }
}