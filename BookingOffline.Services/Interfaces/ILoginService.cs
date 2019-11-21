using BookingOffline.Services.Models;

namespace BookingOffline.Services.Interfaces
{
    public interface ILoginService
    {
        LoginResultModel LoginMiniAlipay(string code);
    }
}