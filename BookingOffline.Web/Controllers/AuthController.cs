using BookingOffline.Services;
using BookingOffline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingOffline.Web.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public AuthController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpGet("login/alipay")]
        [AllowAnonymous]
        public IActionResult LoginAlipay([FromQuery]string code)
        {
            var response = _loginService.LoginMiniAlipay(code);
            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }

        [HttpGet("login/wechat")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWechat([FromQuery]string code)
        {
            var response = await _loginService.LoginMiniWechatAsync(code);
            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }
    }
}
