using BookingOffline.Services.Models;
using System.Threading.Tasks;

namespace BookingOffline.Services.Interfaces
{
    public interface ILoginService
    {
        LoginResultModel LoginMiniAlipay(string code);
        Task<LoginResultModel> LoginMiniWechatAsync(string code);
    }
}