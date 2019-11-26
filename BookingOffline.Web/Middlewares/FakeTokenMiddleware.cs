using BookingOffline.Common;
using BookingOffline.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingOffline.Web.Middlewares
{
    public class FakeTokenMiddleware
    {
        private readonly RequestDelegate _next;
        public FakeTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJ5dWZlbGl4IiwiYmY6YWxpYmFiYVVzZXJJZCI6IjEyMzRhIiwiYmY6YWxpcGF5VXNlcklkIjoiMTIzNDU2YWJjIiwibmJmIjoxNTc0Nzc2NTQ2LCJleHAiOjE1NzUzODEzNDYsImlhdCI6MTU3NDc3NjU0Nn0._OqAiVWbe-ScalT6_-Okz066Q6BWsdPL7MezA4Acxe4";
            httpContext.Request.Headers.Add("Authorization", "Bearer " + token);
            await _next(httpContext);

        }
    }
}
