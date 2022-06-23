using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string TotalPriceTry => TotalPrice.ToString("c2");
    }
}
