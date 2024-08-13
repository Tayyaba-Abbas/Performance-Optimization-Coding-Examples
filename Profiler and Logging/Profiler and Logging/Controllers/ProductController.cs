using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Profiler_and_Logging.Models;
using System.Diagnostics;

namespace Profiler_and_Logging.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly List<Product> _products;
        private readonly List<Order> _orders;
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _memoryCounter;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            // Initialize performance counters
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _memoryCounter = new PerformanceCounter("Memory", "Available MBytes");

            // Initialize with sample data
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 999.99m },
                new Product { Id = 2, Name = "Smartphone", Price = 499.99m },
                new Product { Id = 3, Name = "Headphones", Price = 79.99m },
                new Product { Id = 4, Name = "Monitor", Price = 199.99m },
                new Product { Id = 5, Name = "Keyboard", Price = 29.99m }
            };

            _orders = new List<Order>
            {
                new Order { Id = 1, ProductId = 1, UserId = 1, OrderDate = new DateTime(2024, 8, 1) },
                new Order { Id = 2, ProductId = 2, UserId = 2, OrderDate = new DateTime(2024, 8, 2) },
                new Order { Id = 3, ProductId = 3, UserId = 1, OrderDate = new DateTime(2024, 8, 3) },
                new Order { Id = 4, ProductId = 1, UserId = 3, OrderDate = new DateTime(2024, 8, 4) },
                new Order { Id = 5, ProductId = 4, UserId = 2, OrderDate = new DateTime(2024, 8, 5) },
                new Order { Id = 6, ProductId = 5, UserId = 4, OrderDate = new DateTime(2024, 8, 6) }
            };
        }

        // Action method to display a list of products
        public IActionResult Index()
        {
            _logger.LogInformation("Fetching the list of products.");

            // Log performance metrics
            var cpuUsage = _cpuCounter.NextValue();
            var availableMemory = _memoryCounter.NextValue();

            _logger.LogInformation("CPU Usage: {CpuUsage}%", cpuUsage);
            _logger.LogInformation("Available Memory: {AvailableMemory} MB", availableMemory);

            var products = _products;
            return View(products);
        }

        // Action method to display details of a specific product based on the order
        public IActionResult Details(int id)
        {
            _logger.LogInformation("Fetching details for order with ID {OrderId}.", id);

            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                _logger.LogWarning("Order with ID {OrderId} not found.", id);
                return NotFound();
            }

            var product = _products.FirstOrDefault(p => p.Id == order.ProductId);
            if (product == null)
            {
                _logger.LogWarning("Product associated with Order ID {OrderId} not found.", id);
                return NotFound();
            }

            // Log performance metrics
            var cpuUsage = _cpuCounter.NextValue();
            var availableMemory = _memoryCounter.NextValue();

            _logger.LogInformation("CPU Usage: {CpuUsage}%", cpuUsage);
            _logger.LogInformation("Available Memory: {AvailableMemory} MB", availableMemory);

            ViewBag.Product = product;
            ViewBag.Order = order;
            return View();
        }
    }
}
