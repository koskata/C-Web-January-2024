using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace ShoppingListApp.Data.Models
{
    public class ProductNote
    {
        [Key]
        [Comment("Product Note Identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Content must be between 1 and 150 characters!")]
        public string Content { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]

        public Product Product { get; set; }
    }
}
