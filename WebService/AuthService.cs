using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using rawdata_portfolioproject_2;

namespace WebService
{
    public class AuthService
    {
        private readonly RequestDelegate _next;

        public AuthService(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDataService dataService)
        {
            Program.CurrentProfile = null;
            var auth = context.Request.Headers["Authorization"];
            if (auth != StringValues.Empty)
            {
                Program.CurrentProfile = dataService.GetProfile(auth);
            }

            await _next(context);
        }
    }
}