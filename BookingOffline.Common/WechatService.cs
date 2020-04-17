using BookingOffline.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingOffline.Common
{
    public class WechatService : IWechatService
    {
        public async Task<WechatLoginResultModel> GetUserIdByCode(string authCode)
        {
            var client = new HttpClient();
            var url = "https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code";
            var streamTask = client.GetStreamAsync(url);
            var response = await JsonSerializer.DeserializeAsync<WechatLoginResultModel>(await streamTask);
            return response;
        }
    }
}
