using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpPost("remove")]
        public ActionResult RemoveOrder([FromQuery]string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_service.RemoveOrder(orderId, userId))
                return Ok();
            else
                throw new Exception($"Failed to remove order: {orderId}");
        }

        /// <summary>
        /// Lock an order if it is unlockeds
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("{orderId}/lock")]
        public ActionResult LockOrder([FromRoute]string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _service.LockOrder(orderId, userId);
            return Ok();
        }

        /// <summary>
        /// Unlock an order if it is locked
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("{orderId}/unlock")]
        public ActionResult UnlockOrder([FromRoute]string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _service.UnlockOrder(orderId, userId);
            return Ok();
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
        public ActionResult GetOrders(int page=1, int size=10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _service.GetOrders(userId, page, size);
            if (orders.TotalCount > 0)
            {
                return Ok(orders);
            }

            return NotFound();
        }
    }
}
