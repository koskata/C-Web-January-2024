using Microsoft.AspNetCore.Identity;
using SoftUniBazar.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.DataConstants;

namespace SoftUniBazar.Models
{
    public class AdViewModel
    {

        public AdViewModel(int id, string name, string imageUrl, DateTime createdOn, string category, string description, decimal price, string owner)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            CreatedOn = createdOn.ToString(DateFormat);
            Category = category;
            Description = description;
            Price = price;
            Owner = owner;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(AdNameMaxLength, MinimumLength = AdNameMinLength, ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string CreatedOn { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(AdDescriptionMaxLength, MinimumLength = AdDescriptionMinLength, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = RequiredErrorMessage)]
        public decimal Price { get; set; }


        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Owner { get; set; } = string.Empty;
        
    }
}
