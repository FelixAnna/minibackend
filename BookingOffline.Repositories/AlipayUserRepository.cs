using BookingOffline.Entities;
using System;
using System.Linq;

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

        public bool Delete(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable FindAll(string userId)
        {
            throw new NotImplementedException();
        }

        public AlipayUser FindById(string alipayUserId)
        {
            var user = _context.AlipayUsers.FirstOrDefault(x => x.AlipayUserId == alipayUserId);
            return user;
        }
    }
}
