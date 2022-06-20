using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Constants;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public string UserId => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public string AnonymousId => HttpContext.Request.Cookies[Constant.BASKET_COOKIENAME];

        public string BuyerId => UserId ?? AnonymousId;

        public BasketViewModelService(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddItemToBasketAsync(int productId, int quantity)
        {
            var buyerId = BuyerId ?? CreateAnonymousId();
            Basket basket = await _basketService.AddItemToBasketAsync(buyerId, productId, quantity);
            return basket.Items.Sum(x => x.Quantity);
        }

        private string CreateAnonymousId()
        {
            string newId = Guid.NewGuid().ToString();
            HttpContext.Response.Cookies.Append(Constant.BASKET_COOKIENAME, newId, new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                IsEssential = true
            });
            return newId;
        }

        public async Task<NavBasketViewModel> GetNavBasketViewModelAsync()
        {
            return new NavBasketViewModel() { TotalItems = await _basketService.GetBasketItemCountAsync(BuyerId) };
        }

        public async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            var basket = await _basketService.GetBasketAsync(BuyerId);

            if (basket == null) return null;

            return new BasketViewModel()
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(x => new BasketItemViewModel()
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    PictureUri = x.Product.PictureUri,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.Product.Price
                }).ToList()
            };
        }

        public async Task DeleteBasketAsync()
        {
            await _basketService.DeleteBasketAsync(BuyerId);
        }

        public async Task DeleteBasketItemAsync(int basketItemId)
        {
            await _basketService.DeleteBasketItemAsync(BuyerId, basketItemId);
        }
    }
}
