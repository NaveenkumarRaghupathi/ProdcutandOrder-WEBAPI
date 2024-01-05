using Order.BusinessLayer.Interfaces;
using Order.DataAccessLayer.Entities;
using Order.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.BusinessLayer.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productRepository;

        public ProductsService(IProductsRepository customerRepository)
        {
            _productRepository = customerRepository;
        }

        public async Task<Products> AddProductsAsync(Products products)
        {
            Random generator = new Random();
            String _productCode = generator.Next(0, 1000000).ToString("D6");
            products.ProductCode = string.IsNullOrEmpty(products.ProductCode) ? _productCode : products.ProductCode;
            return await _productRepository.AddProductsAsync(products);
        }

        public Task<bool> DeleteProductsAsync(int id)
        {
            return _productRepository.DeleteProductsAsync(id);
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();

        }

        public async Task<Products> GetProductsByIdAsync(int id)
        {
            return await _productRepository.GetProductsByIdAsync(id);
        }

        public Task<bool> UpdateProductsAsync(Products products)
        {
            return _productRepository.UpdateProductsAsync(products);
        }
    }

    // other methods for CRUD operations
}

