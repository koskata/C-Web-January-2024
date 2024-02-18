using Microsoft.VisualBasic;

using SoftUniBazar.Data.Models;

using System.ComponentModel.DataAnnotations;

using static SoftUniBazar.Data.DataConstants;

namespace SoftUniBazar.Models
{
    public class AdFormViewModel
    {
        //public AdFormViewModel(string name, string imageUrl, string category, string description, decimal price)
        //{
        //    Name = name;
        //    ImageUrl = imageUrl;
        //    Category = category;
        //    Description = description;
        //    Price = price;
        //}


        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(AdNameMaxLength, MinimumLength = AdNameMinLength, ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(AdDescriptionMaxLength, MinimumLength = AdDescriptionMinLength, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int CategoryId { get; set; }

        public IList<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
