﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public AddressDto Address { get; set; }
        public DateTime CreatedDate { get; private set; }
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItemDtos { get; set; }

    }
}
