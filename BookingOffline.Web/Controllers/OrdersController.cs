using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookingOffline.Web.Controllers
{
    [Authorize]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Create a new order (with no orderItem)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult CreateOrder([FromBody]OrderModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _service.CreateOrder(userId, model);
            return Ok(order);
        }

        /// <summary>
        /// Remove a order and it related orderItem
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult RemoveOrder(string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_service.RemoveOrder(orderId, userId))
                return Ok();
            else
                throw new Exception($"Failed to remove order: {orderId}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetOrder(string orderId)
        {
            var order = _service.GetOrder(orderId);
            if (order != null)
            {
                return Ok(order);
            }

            return NotFound();
        }

        [HttpGet("list")]
        public ActionResult GetOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _service.GetOrders(userId);
            if (orders.Any())
            {
                return Ok(orders);
            }

            return NotFound();
        }
    }
}
