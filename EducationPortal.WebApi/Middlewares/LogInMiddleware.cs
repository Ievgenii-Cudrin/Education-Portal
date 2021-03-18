using EducationPortal.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Middlewares
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogInService logInService;

        public LoginMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogInService logInService)
        {
            this.logInService = logInService;
            bool success = await this.logInService.LogIn("Tima", "1612");
            await _next.Invoke(context);
        }
    }
}
