using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Order.DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.DataAccessLayer.Entities;
using Order.Contracts.V1;

namespace Order.DataAccessLayer.Interfaces
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrdersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Orders> AddOrdersAsync(Orders orders)
        {
            try {
                if (orders != null && !string.IsNullOrEmpty(orders.ProductCode))
                {
                    var productDetails = _dbContext.Products.FirstOrDefault(x => x.ProductCode == orders.ProductCode && x.Id == orders.ProductId
                    && x.Status == "Active");
                    if (productDetails!= null && productDetails.StockQty - orders.OrderQty >= 0)
                    { 
                        Random generator = new Random();
                        int length = 6;
                        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                        orders.OrderCode = new string(Enumerable.Repeat(chars, length)
                            .Select(s => s[generator.Next(s.Length)]).ToArray());
                        orders.TotalAmount = productDetails.Price * orders.OrderQty;
                        orders.Status = "Active";
                        orders.CreatedDate = DateTime.Now.Date;
                        _dbContext.Orders.Add(orders);
                        _dbContext.SaveChanges();

                        productDetails.StockQty = productDetails.StockQty - orders.OrderQty;
                        productDetails.Modifieddate = DateTime.Now.Date;
                        _dbContext.Entry(productDetails).CurrentValues.SetValues(productDetails);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        orders.OrderQty = productDetails != null ? productDetails.StockQty - orders.OrderQty : -1;
                        orders.Status = "InsufficientQty";
                        return orders;
                    }
                }
            }
            catch(Exception ex) { }
            return await _dbContext.Orders.FirstOrDefaultAsync(x=> x.ProductCode == orders.ProductCode);
        }

        public async Task<bool> CancelOrdersAsync(int id, string orderCode)
        {
            bool isOrderCancelled = false;
            try {
                var getOrderDetails = await _dbContext.Orders.FirstOrDefaultAsync(x=> x.Id == id && x.OrderCode == orderCode && x.Status == "Active");
                if( getOrderDetails != null )
                {
                    var getProductDetails = _dbContext.Products.Where(x => x.Id == getOrderDetails.ProductId &&
                    x.ProductCode == getOrderDetails.ProductCode).FirstOrDefault();
                    if (getProductDetails != null)
                    {
                        getOrderDetails.Status = "Cancelled";
                        getOrderDetails.ModifiedDate = DateTime.Now.Date;
                        _dbContext.Entry(getOrderDetails).CurrentValues.SetValues(getOrderDetails);
                    
                        getProductDetails.StockQty = getProductDetails != null ? getProductDetails.StockQty + getOrderDetails.OrderQty : 0;
                        getProductDetails.Modifieddate = DateTime.Now.Date;
                        _dbContext.Entry(getProductDetails).CurrentValues.SetValues(getProductDetails);
                        _dbContext.SaveChanges();
                        isOrderCancelled = true;
                    }

                }
            }
            catch(Exception ex) { }
            return isOrderCancelled;
        }

        public Task DeleteOrdersAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync()
        {
            var ss = _dbContext.Orders.ToList();
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Orders> GetOrdersByIdAsync(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public Task UpdateOrdersAsync(Orders orders)
        {
            throw new NotImplementedException();
        }
    }
}
