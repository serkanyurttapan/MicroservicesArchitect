﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public ServiceApi Catalog { get; set; }
        public ServiceApi PhotoStock { get; set; }
        public ServiceApi Basket { get; set; }
        public ServiceApi Discount { get; set; }

    }
    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
