using Newtonsoft.Json;

namespace BookingOffline.Services.Models
{
    public class LoginResultModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("token")]
        public string BOToken { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }
    }
}
