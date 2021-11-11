using System;
using System.Threading.Tasks;
using OrdersApi.Models;
using System.Collections.Generic;

namespace OrdersApi.Persistence
{
    public interface IOrderRepository 
    {
        Task<Order> GetOrderAsync(Guid orderId);

        Task<IEnumerable<Order>> GetOrdersAsync();

        Task RegisterOrderAsync(Order order);

        Order GetOrder(Guid orderId);

        void UpdateOrder(Order order);

        void SaveChanges();

        Task SaveChangesAsync();

        void RegisterOrder(Order order);
    }
}