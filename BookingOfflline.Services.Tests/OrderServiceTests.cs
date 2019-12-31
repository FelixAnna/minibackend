﻿using BookingOffline.Entities;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services;
using BookingOffline.Services.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingOfflline.Services.Tests
{
    public class OrderServiceTests
    {
        private ILogger<OrderService> _logger;
        private IOrderRepository _orderRepo;
        private IAlipayUserRepository _userRepository;
        private OrderService _service;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger<OrderService>>();
            _orderRepo = A.Fake<IOrderRepository>();
            _userRepository = A.Fake<IAlipayUserRepository>();

            _service = new OrderService(_orderRepo, _userRepository, _logger);
        }

        [Test]
        public void CreateOrder_WhenOrderExists_ThenFailed()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(true);
            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).Returns(fakeOrder);

            Assert.Throws<Exception>(() => _service.CreateOrder("anyuser", new OrderModel()));

            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepo.Create(A<Order>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void CreateOrder_WhenOrderNotExists_ThenSuccess()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(false);
            var fakeNewOrder = FakeDataHelper.GetFakeOrder(true);
            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).Returns(fakeOrder);
            A.CallTo(() => _orderRepo.Create(A<Order>.Ignored)).Returns(fakeNewOrder);

            var result = _service.CreateOrder("anyuser", new OrderModel());

            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepo.Create(A<Order>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.FindAll(A<string[]>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.NotNull(result);
        }

        [Test]
        public void GetOrder_WhenOrderExists_ThenSuccess()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(true);
            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).Returns(fakeOrder);
            A.CallTo(() => _userRepository.FindAll(A<string[]>.Ignored)).Returns(new List<AlipayUser>().AsQueryable());

            var result = _service.GetOrder("anyId");

            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.FindAll(A<string[]>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.NotNull(result);
        }

        [Test]
        public void GetOrder_WhenOrderNotExists_ThenFailed()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(false);
            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).Returns(fakeOrder);

            var result = _service.GetOrder("anyId");

            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.FindAll(A<string[]>.Ignored)).MustNotHaveHappened();

            Assert.IsNull(result);
        }

        [Test]
        public void GetOrders_WhenOrderExists_ThenSuccess()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(true);
            A.CallTo(() => _orderRepo.FindAll(A<string>.Ignored)).Returns(new List<Order>() { fakeOrder }.AsQueryable());
            A.CallTo(() => _userRepository.FindAll(A<string[]>.Ignored)).Returns(new List<AlipayUser>().AsQueryable());

            var result = _service.GetOrders("anyId");

            A.CallTo(() => _orderRepo.FindAll(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.FindAll(A<string[]>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.NotNull(result);
            Assert.IsTrue(result.TotalCount == 1);
        }

        [Test]
        public void GetOrders_WhenOrderNotExists_ThenReturnEmpty()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(false);
            A.CallTo(() => _orderRepo.FindAll(A<string>.Ignored)).Returns(new List<Order>() { }.AsQueryable());
            A.CallTo(() => _userRepository.FindAll(A<string[]>.Ignored)).Returns(new List<AlipayUser>().AsQueryable());

            var result = _service.GetOrders("anyId");

            A.CallTo(() => _orderRepo.FindAll(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.FindAll(A<string[]>.Ignored)).MustNotHaveHappened();

            Assert.NotNull(result);
            Assert.IsTrue(result.TotalCount == 0);
        }

        [Test]
        public void RemoveOrder_WhenNoPermission_ThenFailed()
        {
            A.CallTo(() => _orderRepo.Delete(A<string>.Ignored, A<string>.Ignored)).Returns(false);

            Assert.Throws<Exception>(() => _service.RemoveOrder("anyId", "anyUserId"));

            A.CallTo(() => _orderRepo.Delete(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void RemoveOrder_WhenHavePermission_ThenSuccess()
        {
            A.CallTo(() => _orderRepo.Delete(A<string>.Ignored, A<string>.Ignored)).Returns(true);



            var result = _service.RemoveOrder("anyId", "anyUserId");

            A.CallTo(() => _orderRepo.Delete(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.IsTrue(result);
        }

        [Test]
        public void LockOrder_WhenNotExists_ThenFailed()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(false);
            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).Returns(fakeOrder);

            var result = _service.LockOrder("anyId", "anyUserId").Result;

            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepo.LockOrderAsync(A<Order>.Ignored)).MustNotHaveHappened();

            Assert.IsFalse(result);
        }

        [Test]
        public void LockOrder_WhenHavePermission_ThenSuccess()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(true);
            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).Returns(fakeOrder);

            var result = _service.LockOrder("anyId", "anyUserId").Result;

            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepo.LockOrderAsync(A<Order>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.IsTrue(result);
        }

        [Test]
        public void UnlockOrder_WhenNotExists_ThenFailed()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(false);
            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).Returns(fakeOrder);

            var result = _service.UnlockOrder("anyId", "anyUserId").Result;

            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepo.UnlockOrderAsync(A<Order>.Ignored)).MustNotHaveHappened();

            Assert.IsFalse(result);
        }

        [Test]
        public void UnlockOrder_WhenHavePermission_ThenSuccess()
        {
            var fakeOrder = FakeDataHelper.GetFakeOrder(true);
            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).Returns(fakeOrder);

            var result = _service.UnlockOrder("anyId", "anyUserId").Result;

            A.CallTo(() => _orderRepo.FindById(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepo.UnlockOrderAsync(A<Order>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.IsTrue(result);
        }
    }
}