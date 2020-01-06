using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingOffline.Services.Tests
{
    public class UserServiceTests
    {
        private ILogger<UserService> _logger;
        private IAlipayUserRepository _userRepo;
        private UserService _service;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger<UserService>>();
            _userRepo = A.Fake<IAlipayUserRepository>();

            _service = new UserService(_userRepo, _logger);
        }

        [Test]
        public void GetUserInfo_WhenExists_ThenSuccess()
        {
            var fakeUser = FakeDataHelper.GetFakeAlipayUserById(true);
            A.CallTo(() => _userRepo.FindById(A<string>.Ignored)).Returns(fakeUser);

            var result = _service.GetUserInfo("anyId");

            A.CallTo(() => _userRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
        }

        [Test]
        public void GetUserInfo_WhenNotExists_ThenFailed()
        {
            var fakeUser = FakeDataHelper.GetFakeAlipayUserById(false);
            A.CallTo(() => _userRepo.FindById(A<string>.Ignored)).Returns(fakeUser);

            Assert.Throws<NullReferenceException>(() => _service.GetUserInfo("anyId"));

            A.CallTo(() => _userRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void UpdateAlipayUserAsync_WhenNotExists_ThenFailed()
        {
            var fakeUser = FakeDataHelper.GetFakeAlipayUserById(false);
            A.CallTo(() => _userRepo.FindById(A<string>.Ignored)).Returns(fakeUser);

            var result = _service.UpdateAlipayUserAsync("anyId", "anyName", "anyPhoto").Result;

            A.CallTo(() => _userRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepo.UpdateAsync(A<AlipayUser>.Ignored)).MustNotHaveHappened();

            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateAlipayUserAsync_WhenHavePermission_ThenSuccess()
        {
            var fakeUser = FakeDataHelper.GetFakeAlipayUserById(true);
            A.CallTo(() => _userRepo.FindById(A<string>.Ignored)).Returns(fakeUser);

            var result = _service.UpdateAlipayUserAsync("anyId", "anyName", "anyPhoto").Result;

            A.CallTo(() => _userRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepo.UpdateAsync(A<AlipayUser>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.IsTrue(result);
        }
    }
}
