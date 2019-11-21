using BookingOffline.Services.Interfaces;
using BookingOffline.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost()]
        public ActionResult CreateOrder([FromBody]OrderModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _service.CreateOrder(userId, model);
            return Ok(order);
        }

        [HttpDelete]
        public ActionResult RemoveOrder(string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_service.RemoveOrder(orderId, userId))
                return Ok();
            else
                return Problem(statusCode: 500);
        }

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
