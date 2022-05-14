using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto> BasketItemDtos { get; set; }

        public decimal TotalPrice
        {
            get => BasketItemDtos.Sum(x => x.Price * x.Quantity);
        }
    }
}
