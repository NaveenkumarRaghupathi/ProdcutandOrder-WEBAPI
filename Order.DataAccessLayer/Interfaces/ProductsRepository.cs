using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Order.DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.DataAccessLayer.Entities;
using System.Collections;
using Azure.Core;

namespace Order.DataAccessLayer.Interfaces
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Products> AddProductsAsync(Products products)
        {
            Products prod = new Products();
            try
            {
                if (products.ProductCode != null)
                {
                    prod.ProductCode = products.ProductCode;
                    prod.ProductName = products.ProductName;
                    prod.ProductDescription = products.ProductDescription;
                    prod.Price = products.Price;
                    prod.StockQty = products.StockQty;
                    prod.Status = "Active";
                    prod.Createddate = DateTime.Now.Date;
                    prod.Modifieddate = DateTime.Now.Date;
                    _dbContext.Products.Add(prod);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex) { }
            return await _dbContext.Products.Where(x => x.ProductCode == products.ProductCode).FirstAsync();
        }

        public async Task<bool> DeleteProductsAsync(int id)
        {
            bool isDeleted = false;
            try {
                if (id > 0)
                {
                    var removeProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                    if (removeProduct != null)
                    {
                        _dbContext.Products.Remove(removeProduct);  
                        _dbContext.SaveChanges();
                        isDeleted = true;
                    }
                }
            }
            catch (Exception ex) { }
            return isDeleted;
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Products> GetProductsByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<bool> UpdateProductsAsync(Products products)
        {
            bool isUpdated = false;
            try {
                var checkProduct = products.Id > 0 ? await _dbContext.Products.FirstOrDefaultAsync(x=> x.Id == products.Id) : null;
                if (checkProduct != null)
                {
                    checkProduct.ProductCode = products.ProductCode;
                    checkProduct.ProductName = products.ProductName;
                    checkProduct.ProductDescription = products.ProductDescription;
                    checkProduct.Price = products.Price;
                    checkProduct.StockQty = products.StockQty;
                    checkProduct.Status = products.Status;
                    checkProduct.Modifieddate = DateTime.Now.Date;
                    _dbContext.Entry(checkProduct).CurrentValues.SetValues(checkProduct);
                    _dbContext.SaveChanges();
                    isUpdated = true;
                }
            }
            catch (Exception ex) { }
            return isUpdated;
        }
        // other methods for CRUD operations
    }
}
