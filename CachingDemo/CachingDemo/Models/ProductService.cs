using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;

namespace CachingDemo.Models
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly MemoryCache _cache;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
            _cache = MemoryCache.Default;
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
            _cache.Remove("PopularProducts"); // Clear cache
        }

        public List<Product> GetPopularProductsWithoutCache()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var popularProducts = _productRepository.GetPopularProducts();
            stopwatch.Stop();
            // Optionally log the time taken here if needed
            return popularProducts;
        }

        public List<Product> GetPopularProductsWithCache()
        {
            string cacheKey = "PopularProducts";
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (_cache.Contains(cacheKey))
            {
                stopwatch.Stop();
                return (List<Product>)_cache.Get(cacheKey);
            }

            var popularProducts = _productRepository.GetPopularProducts();
            _cache.Set(cacheKey, popularProducts, DateTimeOffset.Now.AddMinutes(10)); // Cache for 10 minutes
            stopwatch.Stop();
            return popularProducts;
        }
    }
}

