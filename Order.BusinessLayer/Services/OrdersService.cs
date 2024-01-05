using Order.BusinessLayer.Interfaces;
using Order.DataAccessLayer.Entities;
using Order.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Order.Contracts.ApiRoutes;

namespace Order.BusinessLayer.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Orders> AddOrdersAsync(Orders orders)
        {
            return await _ordersRepository.AddOrdersAsync(orders);
        }

        public Task<bool> CancelOrdersAsync(int id,string orderCode)
        {
            return _ordersRepository.CancelOrdersAsync(id, orderCode);
        }

        public Task DeleteOrdersAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync()
        {
            return await _ordersRepository.GetOrdersAsync();
        }

        public async Task<Orders> GetOrdersByIdAsync(int id)
        {
            return await _ordersRepository.GetOrdersByIdAsync(id);
        }

        public Task UpdateOrdersAsync(Orders orders)
        {
            throw new NotImplementedException();
        }
    }

    // other methods for CRUD operations
}

