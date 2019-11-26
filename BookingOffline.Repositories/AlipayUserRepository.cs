using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookingOffline.Repositories
{
    public class AlipayUserRepository : IAlipayUserRepository
    {
        private readonly SQLiteDBContext _context;
        public AlipayUserRepository(SQLiteDBContext context)
        {
            _context = context;
        }

        public AlipayUser Create(AlipayUser user)
        {
            //db insert new user
            var result = _context.AlipayUsers.Add(user);
            _context.SaveChanges();
            return result.Entity;
        }

        public bool Delete(string key, string userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AlipayUser> FindAll(params string[] userIds)
        {
            var user = _context.AlipayUsers.Where(x => userIds.Contains(x.Id));
            return user;
        }

        public AlipayUser FindByAlipayId(string alipayUserId)
        {
            var user = _context.AlipayUsers.FirstOrDefault(x => x.AlipayUserId == alipayUserId);
            return user;
        }

        public async Task UpdateAsync(string userId, string nickName, string photo)
        {
            var user = _context.AlipayUsers.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.AlipayName = nickName;
                user.AlipayPhoto = photo;

                _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
