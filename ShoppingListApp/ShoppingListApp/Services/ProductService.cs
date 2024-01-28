using Microsoft.EntityFrameworkCore;

using ShoppingListApp.Contracts;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Models;

namespace ShoppingListApp.Services
{
    public class ProductService : IProductService
    {
        private readonly ShoppingListAppDbContext context;

        public ProductService(ShoppingListAppDbContext _context)
        {
            context = _context;
        }

        public async Task AddProductAsync(ProductViewModel model)
        {
            var entity = new Product()
            {
                Name = model.Name
            };

            await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException("Invalid product id!");
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return
                await
                context.Products
                .AsNoTracking()
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();

        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException("No Product with this id!");
            }

            return new ProductViewModel()
            {
                Id = id,
                Name = product.Name,
            };
        }

        public async Task UpdateProductAsync(ProductViewModel model)
        {

            var entity = await context.Products.FindAsync(model.Id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid id");
            }

            entity.Name = model.Name;

            await context.SaveChangesAsync();
        }
    }
}
