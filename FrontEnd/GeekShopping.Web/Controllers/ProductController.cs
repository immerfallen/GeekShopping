using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            IEnumerable<ProductModel> products = await _productService.FindAllProducts();
            return View(products);
        }

        public  ActionResult ProductCreate()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate([FromBody]ProductModel product)
        {
            if (ModelState.IsValid) 
            {
                var res = await _productService.CreateProduct(product);
                if (res != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(product);
            
        }
    }
}
