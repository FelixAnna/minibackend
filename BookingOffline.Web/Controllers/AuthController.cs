﻿using BookingOffline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingOffline.Web.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public AuthController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpGet("login/alipay")]
        [AllowAnonymous]
        public IActionResult Login(string code)
        {
            var response = _loginService.LoginMiniAlipay(code);
            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }
    }
}
