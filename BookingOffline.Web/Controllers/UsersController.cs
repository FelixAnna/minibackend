using BookingOffline.Services;
using BookingOffline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingOffline.Web.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [Authorize]
        [HttpPost("alipay")]
        public async Task<IActionResult> UpdateAlipayUserAsync([FromQuery]string name, [FromQuery]string photo)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _userService.UpdateAlipayUserAsync(userId, name, photo))
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize]
        [HttpGet("alipay/info")]
        public IActionResult GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userService.GetUserInfo(userId);

            return Ok(user);
        }
    }
}
