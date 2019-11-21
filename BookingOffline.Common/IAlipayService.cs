using Alipay.AopSdk.Core.Response;

namespace BookingOffline.Common
{
    public interface IAlipayService
    {
        AlipaySystemOauthTokenResponse GetUserIdByCode(string authCode);
    }
}