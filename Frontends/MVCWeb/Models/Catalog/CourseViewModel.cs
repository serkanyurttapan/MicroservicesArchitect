﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Models.Catalog
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Picture { get; set; }
        public string StockPictureUrl { get; set; }
        public string UserId { get; set; }
        public FeatureViewModel Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public string Description { get; set; }
        public string DescriptionFormat
        {
            get
            {
                if (Description != null)
                {
                    return Description.Length > 100 ? $"{Description.Substring(0, 100)}..." : Description;
                }
                return Description;
            }
        }
    }
}
