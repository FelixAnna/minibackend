using BookingOffline.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingOffline.Common
{
    public interface IWechatService
    {
        Task<WechatLoginResultModel> GetUserIdByCode(string authCode);
    }
}
