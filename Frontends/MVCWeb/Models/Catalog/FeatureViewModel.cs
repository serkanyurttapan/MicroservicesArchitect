using System.ComponentModel.DataAnnotations;

namespace MVCWeb.Models.Catalog
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs süre")]
        [Required]
        public int Duration { get; set; }
    }
}