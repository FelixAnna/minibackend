using BookingOffline.Services;
using BookingOffline.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            if (await _userService.UpdateAlipayUserAsync(userId, name, photo))
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize]
        [HttpGet("alipay/info")]
        public IActionResult GetAlipayUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userService.GetAlipayUserInfo(userId);

            return Ok(user);
        }

        [Authorize]
        [HttpPost("wechat")]
        public async Task<IActionResult> UpdateWechatUserAsync([FromBody]UserModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(model?.NickName))
            {
                return BadRequest();
            }

            if (await _userService.UpdateWechatUserAsync(userId, model))
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize]
        [HttpGet("wechat/info")]
        public IActionResult GetWechatUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userService.GetWechatUserInfo(userId);

            return Ok(user);
        }
    }
}
