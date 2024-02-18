using Microsoft.AspNetCore.Mvc;

using ShoppingListApp.Contracts;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Models;

namespace ShoppingListApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new ProductViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            await productService.AddProductAsync(model);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetByIdAsync(id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await productService.UpdateProductAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteProductAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
