using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Constants;

namespace Web.Middlewares
{
    public class TransferBasketMiddleware
    {
        private readonly RequestDelegate _next;

        public TransferBasketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IBasketService basketService)
        {
            var anonId = context.Request.Cookies[Constant.BASKET_COOKIENAME];
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (anonId != null && userId != null)
            {
                await basketService.TransferBasketAsync(anonId, userId);
                context.Response.Cookies.Delete(Constant.BASKET_COOKIENAME);
            }
            await _next(context);
        }
    }

    public static class TransferBasketMiddlewareExtensions
    {
        public static IApplicationBuilder UseTransferBasket(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TransferBasketMiddleware>();
        }
    }
}
