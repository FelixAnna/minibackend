using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingOffline.Repositories.SqlServer
{
    public class WechatUserRepository : IWechatUserRepository
    {
        private readonly BODBContext _context;
        public WechatUserRepository(BODBContext context)
        {
            _context = context;
        }

        public WechatUser Create(WechatUser item)
        {
            //db insert new user
            var result = _context.WechatUsers.Add(item);
            _context.SaveChanges();
            return result.Entity;
        }

        public bool Delete(string key, string userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WechatUser> FindAll(params string[] userIds)
        {
            var user = _context.WechatUsers.Where(x => userIds.Contains(x.Id));
            return user;
        }

        public WechatUser FindByOpenId(string openId)
        {
            var user = _context.WechatUsers.FirstOrDefault(x => x.OpenId == openId);
            return user;
        }

        public WechatUser FindById(string userId)
        {
            var user = _context.WechatUsers.FirstOrDefault(x => x.Id == userId);
            return user;
        }

        public async Task UpdateAsync(WechatUser user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
