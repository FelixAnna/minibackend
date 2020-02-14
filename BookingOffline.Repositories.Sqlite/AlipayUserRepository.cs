using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookingOffline.Repositories.Sqlite
{
    public class AlipayUserRepository : IAlipayUserRepository
    {
        private readonly BODBContext _context;
        public AlipayUserRepository(BODBContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
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

        public AlipayUser FindById(string userId)
        {
            var user = _context.AlipayUsers.FirstOrDefault(x => x.Id == userId);
            return user;
        }

        public async Task UpdateAsync(AlipayUser user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
