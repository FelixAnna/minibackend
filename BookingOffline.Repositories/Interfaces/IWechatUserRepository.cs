using BookingOffline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingOffline.Repositories.Interfaces
{
    public interface IWechatUserRepository : IRepository<WechatUser, string>
    {
        IQueryable<WechatUser> FindAll(params string[] userIds);

        Task UpdateAsync(WechatUser user);
        WechatUser FindByOpenId(string openId);
    }
}
