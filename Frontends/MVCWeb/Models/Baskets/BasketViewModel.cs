using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Models.Baskets
{
    public class BasketViewModel
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        private List<BasketItemViewModel> _basketItemViews { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return _basketItemViews.Sum(x => x.GetCurrentPrice * x.Quantity);
            }
        }
        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketItemViews.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                        x.AppliedDiscount(Math.Round((x.Price - discountPrice), 2));
                    });
                }
                return _basketItemViews;
            }
            set
            {
                _basketItemViews = value;
            }
        }
        public bool HasDiscount
        {
            get => !string.IsNullOrEmpty(DiscountCode);
        }
    }
}
