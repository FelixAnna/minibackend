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
    [Route("orders/items")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _service;
        public OrderItemsController(IOrderItemService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Add a new orderItem to an existing order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult CreateOrderItem([FromBody]OrderItemModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _service.CreateOrderItem(userId, model);
            return Ok();
        }

        /// <summary>
        /// Remove orderItem
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns></returns>
        [HttpPost("remove")]
        public ActionResult RemoveOrderItem([FromQuery]int orderItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_service.RemoveOrderItem(orderItemId, userId))
                return Ok();
            else
                throw new Exception($"Failed to remove orderItem: {orderItemId}");
        }

        [HttpGet]
        public ActionResult GetOrderItem(int orderItemId)
        {
            var item = _service.GetOrderItem(orderItemId);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        [HttpGet("list")]
        public ActionResult GetOrderItems(string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = _service.GetOrderItems(orderId);
            if (items.Any())
            {
                return Ok(items);
            }

            return NotFound();
        }
    }
}
