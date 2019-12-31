using BookingOffline.Common;
using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.Extensions.Logging;
using System;

namespace BookingOffline.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly ITokenGeneratorService _tokenService;
        private readonly IAlipayService _alipayService;
        private readonly IAlipayUserRepository _userRepo;

        public LoginService(ITokenGeneratorService tokenService, IAlipayService alipayService, IAlipayUserRepository userRepo, ILogger<LoginService> logger)
        {
            _logger = logger;
            this._tokenService = tokenService;
            this._alipayService = alipayService;
            _userRepo = userRepo;
        }

        public LoginResultModel LoginMiniAlipay(string code)
        {
            var response = _alipayService.GetUserIdByCode(code);
            if (response.IsError)
            {
                _logger.LogError(response.Body);
                return null;
            }

            var alipayUser = _userRepo.FindByAlipayId(response.AlipayUserId);
            if (alipayUser == null)
            {
                alipayUser = _userRepo.Create(new AlipayUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    AlibabaUserId = response.UserId,
                    AlipayUserId = response.AlipayUserId,
                    CreatedAt = DateTime.UtcNow
                });
            }

            var tokenStr = _tokenService.CreateJwtToken(alipayUser);
            var result = new LoginResultModel()
            {
                BOToken = tokenStr,
                AccessToken = response.AccessToken,
                ExpiresIn = response.ExpiresIn,
                ReExpiresIn = response.ReExpiresIn,
                RefreshToken = response.RefreshToken,
                UserId = alipayUser.Id,
                NickName = alipayUser.AlipayName,
                Photo = alipayUser.AlipayPhoto
            };

            return result;
        }
    }
}
