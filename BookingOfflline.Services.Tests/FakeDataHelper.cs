using Alipay.AopSdk.Core.Response;
using BookingOffline.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingOffline.Services.Tests
{
    public static class FakeDataHelper
    {
        public static AlipaySystemOauthTokenResponse GetFakeAlipayResponse(bool success = false)
        {
            if (!success)
            {
                return new Alipay.AopSdk.Core.Response.AlipaySystemOauthTokenResponse()
                {
                    Code = "20000",
                    SubCode = "isp.unknow-error",
                    Msg = "Service Currently Unavailable",
                    SubMsg = "系统繁忙"
                };
            }

            return new AlipaySystemOauthTokenResponse()
            {
                UserId = "anyId",
                AlipayUserId = "anyalipayid",
                AccessToken = "anytoken",
                ExpiresIn = "anytime",
                ReExpiresIn = "anytime",
                RefreshToken = "anyretoken",
            };
        }

        public static AlipayUser GetFakeAlipayUserById(bool success = false)
        {
            if (!success)
            {
                return null;
            }

            return new AlipayUser();
        }

        public static Order GetFakeOrder(bool success = true)
        {
            if (success)
            {
                return new Order() { State = 1, OrderItems = new List<OrderItem>() };
            }

            return null;
        }

        public static OrderItem GetFakeOrderItem(bool success = true)
        {
            if (success)
            {
                return new OrderItem()
                {
                    OrderItemOptions = new List<OrderItemOption>()
                };
            }

            return null;
        }
    }
}
