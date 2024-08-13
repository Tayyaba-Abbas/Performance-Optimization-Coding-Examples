using CachingDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CachingDemo.Models
{
    public class ProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            // Simulated data source with initial data
            _products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Product 1", IsActive = true, Sales = 100, Rating = 4.5 },
            new Product { ProductId = 2, Name = "Product 2", IsActive = true, Sales = 150, Rating = 4.8 },
            new Product { ProductId = 3, Name = "Product 3", IsActive = true, Sales = 200, Rating = 5.0 },
            new Product { ProductId = 4, Name = "Product 4", IsActive = true, Sales = 250, Rating = 3.9 },
            new Product { ProductId = 5, Name = "Product 5", IsActive = true, Sales = 180, Rating = 4.3 }
        };
        }

        // Function to add a new product
        public void AddProduct(Product product)
        {
            // Simulate auto-incrementing the ProductId
            product.ProductId = _products.Max(p => p.ProductId) + 1;
            _products.Add(product);
        }

        // Function to get popular products based on sales and rating
        public List<Product> GetPopularProducts()
        {
            // Simulate a delay to represent a complex query
            Task.Delay(2000).Wait();
            // Simulate a complex query with filtering and sorting
            return _products
                   .Where(p => p.IsActive)
                   .OrderByDescending(p => p.Sales)
                   .ThenByDescending(p => p.Rating)
                   .Take(10)
                   .ToList();
        }
    }
}
