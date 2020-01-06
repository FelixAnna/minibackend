using BookingOffline.Common;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using FakeItEasy;
using BookingOffline.Entities;

namespace BookingOffline.Services.Tests
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
        public void LoginMiniAlipay_WhenAlipayFailed_ThenReturnNull()
        {
            var fakeAlipayResponse = FakeDataHelper.GetFakeAlipayResponse(success: false);
            A.CallTo(() => _alipayService.GetUserIdByCode(A<string>.Ignored)).Returns(fakeAlipayResponse);

            var result = _service.LoginMiniAlipay("anycode");

            Assert.IsNull(result);
            A.CallTo(() => _alipayService.GetUserIdByCode(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepo.FindByAlipayId(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void LoginMiniAlipay_WhenUserNotExists_ThenShouldCreateUser()
        {
            string fakeToken = "fakeToken";
            var fakeAlipayResponse = FakeDataHelper.GetFakeAlipayResponse(success: true);
            var fakeUserResult = FakeDataHelper.GetFakeAlipayUserById(success: false);
            A.CallTo(() => _alipayService.GetUserIdByCode(A<string>.Ignored)).Returns(fakeAlipayResponse);
            A.CallTo(() => _userRepo.FindByAlipayId(A<string>.Ignored)).Returns(fakeUserResult);
            A.CallTo(() => _userRepo.Create(A<AlipayUser>.Ignored)).Returns(new AlipayUser());
            A.CallTo(() => _tokenService.CreateJwtToken(A<AlipayUser>.Ignored)).Returns(fakeToken);

            var result = _service.LoginMiniAlipay("anycode");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.BOToken, fakeToken);
            A.CallTo(() => _alipayService.GetUserIdByCode(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepo.FindByAlipayId(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepo.Create(A<AlipayUser>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _tokenService.CreateJwtToken(A<AlipayUser>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void LoginMiniAlipay_WhenUserExists_ThenShouldNotCreateUser()
        {
            string fakeToken = "fakeToken";
            var fakeAlipayResponse = FakeDataHelper.GetFakeAlipayResponse(success:true);
            var fakeUserResult = FakeDataHelper.GetFakeAlipayUserById(success:true);
            A.CallTo(() => _alipayService.GetUserIdByCode(A<string>.Ignored)).Returns(fakeAlipayResponse);
            A.CallTo(() => _userRepo.FindByAlipayId(A<string>.Ignored)).Returns(fakeUserResult);
            A.CallTo(() => _userRepo.Create(A<AlipayUser>.Ignored)).Returns(fakeUserResult);
            A.CallTo(() => _tokenService.CreateJwtToken(A<AlipayUser>.Ignored)).Returns(fakeToken);

            var result = _service.LoginMiniAlipay("anycode");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.BOToken, fakeToken);
            A.CallTo(() => _alipayService.GetUserIdByCode(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepo.FindByAlipayId(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepo.Create(A<AlipayUser>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _tokenService.CreateJwtToken(A<AlipayUser>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}