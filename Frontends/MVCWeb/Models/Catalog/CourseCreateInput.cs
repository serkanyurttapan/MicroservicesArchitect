using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Models.Catalog
{
    public class CourseCreateInput
    {
        [Display(Name = "Kurs ismi")]
        [Required]
        //fluent validation
        public string Name { get; set; }
        [Display(Name = "Kurs fiyat")]
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Kurs resim")]
        public string Picture { get; set; }
        public string UserId { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Kurs kategori")]
        [Required]
        public string CategoryId { get; set; }
        [Display(Name = "Kurs açıklama")]
        public string Description { get; set; }
        public IFormFile PhotoFormFile { get; set; }
    }
}
