using BookingOffline.Common;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using FakeItEasy;

namespace BookingOfflline.Services.Tests
{
    public class LoginServiceTests
    {
        private ILogger<LoginService> _logger;
        private ITokenGeneratorService _tokenService;
        private IAlipayService _alipayService;
        private IAlipayUserRepository _userRepo;

        private LoginService _service;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger<LoginService>>();
            _tokenService = A.Fake<ITokenGeneratorService>();
            _alipayService = A.Fake<IAlipayService>();
            _userRepo = A.Fake<IAlipayUserRepository>();

            _service = new LoginService(_tokenService, _alipayService, _userRepo, _logger);
        }

        [Test]
        public void LoginMiniAlipay_WhenFailed_ThenError()
        {
            A.CallTo(() => _alipayService.GetUserIdByCode(A<string>.Ignored)).Returns(new Alipay.AopSdk.Core.Response.AlipaySystemOauthTokenResponse() { Code = "error" });

            var result = _service.LoginMiniAlipay(A<string>.Ignored);

            Assert.Pass();
        }
    }
}