using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingOffline.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IAlipayUserRepository _userRepo;

        public UserService(IAlipayUserRepository userRepo, ILogger<UserService> logger)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        public async Task UpdateAlipayUser(string userId, string nickName, string photo)
        {
            await _userRepo.UpdateAsync(userId, nickName, photo);
        }
    }
}
