using Order.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.BusinessLayer.Interfaces
{
    public interface IOrdersService
    {
        Task<IEnumerable<Orders>> GetOrdersAsync();

        Task<Orders> GetOrdersByIdAsync(int id);

        Task<Orders> AddOrdersAsync(Orders orders);

        Task UpdateOrdersAsync(Orders orders);

        Task DeleteOrdersAsync(int id);
        Task<bool> CancelOrdersAsync(int id,string orderCode);
    }
}
