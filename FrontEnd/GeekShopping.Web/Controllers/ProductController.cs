﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            IEnumerable<ProductModel> products = await _productService.FindAllProducts();
            return View(products);
        }
       
        public  ActionResult ProductCreate()
        {            
            return View();
        }

        [Authorize]
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
        
        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.FindProductById(id);
            if(product != null) { return View(product); }
            return NotFound();

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate([FromBody] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.UpdateProduct(product);
                if (res != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(product);

        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> ProductDelete(long id)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.FindProductById(id);
                if (product != null) return View(product);
            }

            return NotFound();

        }

        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.DeleteProduct(product.Id);
                if (res) return RedirectToAction(nameof(ProductIndex));
            }

            return View(product);

        }
    }
}