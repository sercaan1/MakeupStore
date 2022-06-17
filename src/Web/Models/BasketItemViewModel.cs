using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public string UnitPriceTry => UnitPrice.ToString("c2");

        public string PictureUri { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        public string TotalPriceTry => (Quantity * UnitPrice).ToString("c2");
    }
}
