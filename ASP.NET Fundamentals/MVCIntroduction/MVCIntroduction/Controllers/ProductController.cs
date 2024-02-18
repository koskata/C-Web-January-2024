using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using MVCIntroduction.Models;

namespace MVCIntroduction.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> productViewModels = new List<ProductViewModel>()
        {
            new ProductViewModel() {
                Id = 1,
                Name = "Cheese",
                Price = 7,
            },
            new ProductViewModel() {
                Id = 2,
                Name = "Ham",
                Price = 5,
            },
            new ProductViewModel() {
                Id = 3,
                Name = "Bread",
                Price = 2,
            }
        };

        public IActionResult Index(string? keyword)
        {
            if (keyword is null)
            {
                return View(productViewModels);
            }
            else
            {
                List<ProductViewModel> newProducts = new List<ProductViewModel>();

                foreach (var item in productViewModels)
                {
                    if (item.Name.ToLower().Contains(keyword.ToLower()))
                    {
                        newProducts.Add(item);
                    }
                }

                return View(newProducts);
            }
        }

        public IActionResult ById(int id)
        {
            ProductViewModel productViewModel = null;
            if (id < 0 || id > productViewModels.Count())
            {
                ViewBag.Error = "Не съществува такова Id!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                productViewModel = productViewModels.FirstOrDefault(x => x.Id == id);
            }

            return View(productViewModel);
        }

        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            return Json(productViewModels, options);
        }

        public IActionResult AllAsText()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in productViewModels)
            {
                sb.AppendLine($"Product {item.Id}: {item.Name} - {item.Price} lv.");
            }

            return Content(sb.ToString());
        }

        public IActionResult AllAsTextFile()
        {
            var sb = new StringBuilder();

            foreach (var item in productViewModels)
            {
                sb.AppendLine($"Product {item.Id}: {item.Name} - {item.Price} lv.");
            }

            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
        }
    }
}
