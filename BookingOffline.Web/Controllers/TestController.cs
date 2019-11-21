using BookingOffline.Common;
using BookingOffline.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookingOffline.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITokenGeneratorService _tokenService;
        public TestController(ITokenGeneratorService tokenService)
        {
            this._tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpGet("token")]
        public IActionResult Index()
        {
            // authentication successful so generate jwt token
            var tokenStr = _tokenService.CreateJwtToken(new AlipayUser()
            {
                Id = Guid.NewGuid().ToString(),
                AlibabaUserId = "1234a",
                AlipayUserId = "123456abc",
                CreatedAt = DateTime.UtcNow
            });
            return Ok(new { token = tokenStr });
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            return Ok(new { State = "succeed" });
        }
    }
}