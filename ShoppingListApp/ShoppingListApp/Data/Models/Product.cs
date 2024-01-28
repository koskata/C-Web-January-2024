using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace ShoppingListApp.Data.Models
{
    public class Product
    {

        [Key]
        [Comment("Product Identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 symbols!")]
        public string Name { get; set; } = null!;

        public List<ProductNote> ProductNotes { get; set; } = new List<ProductNote>();
    }
}
