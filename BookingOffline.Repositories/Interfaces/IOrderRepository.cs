﻿using System.Linq;
using System.Threading.Tasks;
using BookingOffline.Entities;

namespace BookingOffline.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order, int>
    {
        IQueryable<Order> FindAll(string userId);
        Task UpdateAsync(Order order);
    }
}