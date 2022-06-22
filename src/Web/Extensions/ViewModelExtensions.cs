using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Extensions
{
    public static class ViewModelExtensions
    {
        public static BasketViewModel ToBasketViewModel(this Basket basket)
        {
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
    }
}
