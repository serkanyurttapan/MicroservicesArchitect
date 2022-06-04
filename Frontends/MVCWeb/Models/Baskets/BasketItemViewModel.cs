using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Models.Baskets
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        private decimal? DiscountAppliedPrice { get; set; }

        public decimal GetCurrentPrice
        {
            get
            {
                if (DiscountAppliedPrice != null)
                {
                    if (DiscountAppliedPrice > 0)
                    {
                        return DiscountAppliedPrice.Value;
                    }
                }
                return Price;
            }
        }

        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
    }
}
