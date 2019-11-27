using BookingOffline.Services.Models;
using System.Threading.Tasks;

namespace BookingOffline.Services
{
    public interface IUserService
    {
        Task<bool> UpdateAlipayUserAsync(string userId, string nickName, string photo);
        UserResultModel GetUserInfo(string userId);
    }
}