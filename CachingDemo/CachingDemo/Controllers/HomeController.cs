using CachingDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace Caching.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;

        public HomeController(ProductService productService)
        {
            _productService = productService;
        }

        // Action for the homepage with a button to get popular products
        public IActionResult Index()
        {
            return View();
        }

        // Action to display the popular products with performance logging
        public IActionResult PopularProducts(bool useCache = true)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Product> products;
            if (useCache)
            {
                products = _productService.GetPopularProductsWithCache();
            }
            else
            {
                products = _productService.GetPopularProductsWithoutCache();
            }

            stopwatch.Stop();
            ViewBag.ElapsedTime = stopwatch.ElapsedMilliseconds;
            ViewBag.UseCache = useCache;

            return View(products);
        }

        // Action to display a form for adding a new product
        public IActionResult AddProduct()
        {
            return View();
        }

        // Action to handle the form submission for adding a new product
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productService.AddProduct(product);
            return RedirectToAction("Index");
        }
    }
}
