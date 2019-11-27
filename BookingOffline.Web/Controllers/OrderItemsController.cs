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
            _service.CreateOrderItem(userId, model);
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
            _service.RemoveOrderItem(orderItemId, userId);
            return Ok();
        }
    }
}
