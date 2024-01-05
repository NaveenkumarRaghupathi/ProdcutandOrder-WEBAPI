using Order.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.BusinessLayer.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<Products>> GetProductsAsync();

        Task<Products> GetProductsByIdAsync(int id);

        Task<Products> AddProductsAsync(Products products);

        Task<bool> UpdateProductsAsync(Products products);

        Task<bool> DeleteProductsAsync(int id);
    }
}
