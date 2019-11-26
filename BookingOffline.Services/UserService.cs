﻿using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.Extensions.Logging;
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

        public UserResultModel GetUserInfo(string userId)
        {
            var user = _userRepo.FindById(userId);
            return new UserResultModel()
            {
                Id=user.Id,
                AlipayName=user.AlipayName,
                AlibabaUserId=user.AlibabaUserId,
                AlipayPhoto=user.AlipayPhoto,
                AlipayUserId=user.AlipayUserId,
                CreatedAt=user.CreatedAt
            };
        }

        public async Task UpdateAlipayUser(string userId, string nickName, string photo)
        {
            await _userRepo.UpdateAsync(userId, nickName, photo);
        }
    }
}
