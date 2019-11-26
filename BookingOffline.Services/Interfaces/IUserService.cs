using System.Threading.Tasks;

namespace BookingOffline.Services
{
    public interface IUserService
    {
        Task UpdateAlipayUser(string userId, string nickName, string photo);
    }
}