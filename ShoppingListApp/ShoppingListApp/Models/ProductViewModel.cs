using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Models
{
    public class ProductViewModel
    {
        /// <summary>
        /// Product Identifier
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Product Name
        /// </summary>
        [Required]
        [MinLength(3, ErrorMessage = "Name must be atleast 3 symbols!")]
        public string Name { get; set; }
    }
}
