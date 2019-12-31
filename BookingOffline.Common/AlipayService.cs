﻿using Alipay.AopSdk.Core;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;

namespace BookingOffline.Common
{
    public class AlipayService : IAlipayService
    {
        public AlipayService(string privateKey, string publicKey, string appId)
        {
            this.privateKey = privateKey;
            this.alipayPublicKey = publicKey;
            this.appId = appId;
        }
        private string privateKey;
        private string alipayPublicKey;
        private string appId;
        public AlipaySystemOauthTokenResponse GetUserIdByCode(string authCode)
        {
            IAopClient client = new DefaultAopClient(
                "https://openapi.alipay.com/gateway.do",
                this.appId,  //app_id
                privateKey,
                "json", "1.0", "RSA2",
                alipayPublicKey,
                "utf-8",
                false);
            AlipaySystemOauthTokenRequest request = new AlipaySystemOauthTokenRequest
            {
                GrantType = "authorization_code",
                Code = authCode
            };
            //request.RefreshToken = "201208134b203fe6c11548bcabd8da5bb087a83b";
            AlipaySystemOauthTokenResponse response = client.Execute(request);
            return response;
        }
    }
}
