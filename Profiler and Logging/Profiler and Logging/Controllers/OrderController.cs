using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Profiler_and_Logging.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Profiler_and_Logging.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly List<Product> _products;
        private readonly List<Order> _orders;
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _memoryCounter;

        public OrderController(ILogger<OrderController> logger)
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

        // Action method to display a list of orders
        public IActionResult Index()
        {
            _logger.LogInformation("Fetching list of orders.");

            var orders = _orders;
            // Log performance metrics
            var cpuUsage = _cpuCounter.NextValue();
            var availableMemory = _memoryCounter.NextValue();

            _logger.LogInformation("CPU Usage: {CpuUsage}%", cpuUsage);
            _logger.LogInformation("Available Memory: {AvailableMemory} MB", availableMemory);
            return View(orders);
        }

        // Action method to display details of a specific order
        public IActionResult Details(int id)
        {
            _logger.LogInformation("Fetching details for order with ID {OrderId}.", id);

            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            var product = _products.FirstOrDefault(p => p.Id == order.ProductId);
            if (product == null)
                return NotFound();
            // Log performance metrics
            var cpuUsage = _cpuCounter.NextValue();
            var availableMemory = _memoryCounter.NextValue();

            _logger.LogInformation("CPU Usage: {CpuUsage}%", cpuUsage);
            _logger.LogInformation("Available Memory: {AvailableMemory} MB", availableMemory);
            ViewBag.Product = product;
            return View(order);
        }

        // Action method to create a new order
        [HttpPost]
        public IActionResult CreateOrder(int productId, int userId)
        {
            _logger.LogInformation("Creating a new order for product ID {ProductId}.", productId);

            var order = new Order
            {
                Id = _orders.Max(o => o.Id) + 1, // Generate a new ID
                ProductId = productId,
                UserId = userId,
                OrderDate = DateTime.Now
            };

            _orders.Add(order);
            // Log performance metrics
            var cpuUsage = _cpuCounter.NextValue();
            var availableMemory = _memoryCounter.NextValue();

            _logger.LogInformation("CPU Usage: {CpuUsage}%", cpuUsage);
            _logger.LogInformation("Available Memory: {AvailableMemory} MB", availableMemory);

            return RedirectToAction("Index");

        }
    }
}
