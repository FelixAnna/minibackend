using BookingOffline.Entities;
using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using BookingOffline.Web.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

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
            OrderResultModel order;
            if (User.IsAlipayUser()) 
            {
                order = _service.CreateOrder<AlipayUser>(userId, model);
            }
            else
            {
                order = _service.CreateOrder<WechatUser>(userId, model);
            }

            return Ok(order);
        }

        /// <summary>
        /// Create a new order (with no orderItem)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}/options")]
        public async Task<ActionResult> UpdateOrder(int Id, [FromBody]OrderModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OrderResultModel order;
            if (User.IsAlipayUser())
            {
                order = await _service.UpdateOrderAsync<AlipayUser>(Id, userId, model);
            }
            else
            {
                order = await _service.UpdateOrderAsync<WechatUser>(Id, userId, model);
            }

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        /// <summary>
        /// Remove a order and it related orderItem
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("remove")]
        public ActionResult RemoveOrder([FromQuery]int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _service.RemoveOrder(orderId, userId);
            return Ok();

        }

        /// <summary>
        /// Lock an order if it is unlockeds
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("{orderId}/lock")]
        public async Task<ActionResult> LockOrder([FromRoute]int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.LockOrderAsync(orderId, userId);
            return Ok();
        }

        /// <summary>
        /// Unlock an order if it is locked
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("{orderId}/unlock")]
        public async Task<ActionResult> UnlockOrder([FromRoute]int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _service.UnlockOrderAsync(orderId, userId);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetOrder(int orderId)
        {
            OrderResultModel order;
            if (User.IsAlipayUser())
            {
                order = _service.GetOrder<AlipayUser>(orderId);
            }
            else
            {
                order = _service.GetOrder<WechatUser>(orderId);
            }

            if (order != null)
            {
                return Ok(order);
            }

            return NotFound();
        }

        [HttpGet("list")]
        public ActionResult GetOrders(int page = 1, int size = 10, DateTime? startDate = null, DateTime? endDate = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OrderCollectionResultModel orders;
            if (User.IsAlipayUser())
            {
                orders = _service.GetOrders<AlipayUser>(userId, startDate, endDate, page, size);
            }
            else
            {
                orders = _service.GetOrders<WechatUser>(userId, startDate, endDate, page, size);
            }

            if (orders.TotalCount > 0)
            {
                return Ok(orders);
            }

            return NotFound();
        }
    }
}
