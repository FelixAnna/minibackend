﻿using BookingOffline.Common.Models;
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
        public WechatService(string appId, string secret)
        {
            this.secret = secret;
            this.appId = appId;
        }
        private string secret;
        private string appId;

        public async Task<WechatLoginResultModel> GetUserIdByCode(string authCode)
        {
            var client = new HttpClient();
            var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={this.appId}&secret={this.secret}&js_code={authCode}&grant_type=authorization_code";
            var content = await client.GetStringAsync(url);
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<WechatLoginResultModel>(content);

            return response;
        }
    }
}
