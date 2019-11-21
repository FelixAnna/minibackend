using Newtonsoft.Json;

namespace BookingOffline.Services.Models
{
    public class LoginResultModel
    {
        [JsonProperty("alipay_access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("alipay_expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty("alipay_re_expires_in")]
        public string ReExpiresIn { get; set; }
        [JsonProperty("alipay_refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("bo_user_id")]
        public string UserId { get; set; }

        [JsonProperty("bo_token")]
        public string BOToken { get; set; }
    }
}
