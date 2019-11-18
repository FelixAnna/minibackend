using Alipay.AopSdk.Core;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;

namespace BookingOffline.Common
{
    public class AlipayService
    {
        private const string privateKey = "MIIEpAIBAAKCAQEAxHC73mV4mB3vwIKXGokMi0+/neYVOArgE7EHsynI4Ob+x4vscM6glHcW2Aog9ea9HO6xLkfQloPXE2OCf3ZQWWDngKN2tn9uglU6eIf0b8s69v/IgVvGkJxDoorQgbA/8FIoxBAMbfmmOtNGxUcRoJXNTJjTeVyP6twShp1A4XBt3PXrP+tH47wF4gTAwnndVhWe0CtutuPvIJvTSJFaoAoAzB1Xm/i9fxiGvSmvp8Vq30U92wQZgpNtLA+jlm4qlkembPl/ZdoyqmeNRbLDtFGu0DGYFUHo95K9EA7eubO7Y9dJslNZuMEzzhagyY7x+RceSEigi7HDcAfYJgGpcQIDAQABAoIBAQCq/tcKiJmpEKYalZKi7pmUyx6pfBcMaasUeQ2Sz9SksW8mlI6Ew9jUY7Xm6So341rLTJu/BZR08N7Pade2zG603r2mJKS5x8OSgE4djXshtW7mg1EuCY2XY+8cFmnZ+mot79a8lJu9P4uLuU3pfCvD9aJ4ArBS/FfqwRb/RvOrSIPyYEdYKAp1oLoSa7XLtodufZQ1jLeS9mwcUZoPhbfjodS/5G4OZLwVUspkwBZnHq70MD/k4tRUdR+r7ytdu3icQl6B+03QtEEBcWasnbxL4GexvlBOVoICLWrw+mCX7i+HMkLUnBZ7G5NEuXtL5IzwAUATpZ4nWiIM7Hx/NxABAoGBAOvEQ1j9yBjEYNqF328YYiug8XS5bFnk9drZD6wTwlPn3CRfRSbs2veshTTtN0TFR5PedyRLp9wqB/7fWTfXNk19vIR3zahEqXR1ugMeDrfZUs9FS4aH0Qos+/dxvUpLsM9S7/VY5tlZVbL8ZwDGHbM1r6jNrlEpmiKoOPBY6dhhAoGBANVMe1BqootJhhtMd7UlEK38xj0sY8QC8SKE8i/jkaP9UGUNIYGf/XO2ASNAqRitn+Gd1V6ABNrEz7F/uymrcVip2gcMpDZs0lHDcJTl0SsCUglbIBK3YH+eU8vWFTCL9QgLJtOngqM8HxSLaMJYYak4lqqfzhrpXFVlRTliYisRAoGBAInRkqTkxSFlOlDDTRc+I7TpQfw/oAz0gJtLo5OtgT1XHiLM5jmY329ElCGaQWKcB/lyZCb9asSSdVYR7a9syLuHsrmk5r1kVcJUR0YnPedOzM+I7FtZ7WKLZUcCX+qcUGEBVHARZfDL/gLA9N1I8ned/G8qvKmJLibwO0iUUS5BAoGAUHiDSWqBmlUJwEDQJMowCcQEsk2N8gQ+HwzTJhgP+TN3Yjha/MF0aHQw57DcWtAeMotOVVGtAzAfP1L2NJlWsOGbvO8bAOF7U5eNYM93Y2eDtb3oUsdWI6+C47MH2YHj1r4GTvGyio0DGMgjpLadWa9cwFB7/JEv4ZPMUwL4EFECgYAlFeTVV4S5ft2kR7kzWuKTRWFdLccR89QTiA5BwdCrEd93zL0IGk1B6j8MRE/mUNIxEaQBEBrvMNkLRAL56QbuUYgW2vhe+DcvcbP7aVUaDzMwAUNdkqGgaKxGOSVM6ukfAoXE7xMalo/rw52WJjkTqqJTBrQTr+eZIaVss9PvTg==";
        private const string alipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAs0ybQtwsb1HHlIxKxjBn8JnBuWTODger4uM1PbapZTL6p6hAGTsw4E0WXBLEFSNyWY3Ru0mZxqbklBybvRySnM2RN+8WeB/Brt72vxDtYt5NhdC/zY4IS5kk+VvBPNbAdXtnieftHxd69OqFElB7hjrhJcYm7I0orbydXjKpdK4ErdfPs7AncICSiUVlKFNSYh/5eqLhnREq/2TiD63E/AJ/gLCAHJiNqMx8jL2Qt8Z5DkwhSgC4PXr2fk7q1joo3O0CuxIAG/Do0NZbWSHrQUg0jGN6TF+3B3Jcdl7E8hhecQV5B5NTX/gNctYVho6iTOSUH2bfzohhD3sYBrbkawIDAQAB";
        public AlipaySystemOauthTokenResponse GetUserIdByCode(string authCode)
        {
            IAopClient client = new DefaultAopClient(
                "https://openapi.alipay.com/gateway.do",
                "2019101868499001",  //app_id
                privateKey,
                "json", "1.0", "RSA2",
                alipayPublicKey,
                "utf-8",
                false);
            AlipaySystemOauthTokenRequest request = new AlipaySystemOauthTokenRequest();
            request.GrantType = "authorization_code";
            request.Code = authCode;
            //request.RefreshToken = "201208134b203fe6c11548bcabd8da5bb087a83b";
            AlipaySystemOauthTokenResponse response = client.Execute(request);
            return response;
        }
    }
}
