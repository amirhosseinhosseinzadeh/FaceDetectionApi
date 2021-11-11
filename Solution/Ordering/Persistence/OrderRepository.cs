using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Models;

namespace OrdersApi.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDataContext _dataContext;

        public OrderRepository(OrdersDataContext dataContext)
            => this._dataContext = dataContext;

        public Order GetOrder(Guid orderId)
            => _dataContext.Orders.FirstOrDefault(p => p.OrderId.Equals(orderId));

        public async Task<Order> GetOrderAsync(Guid orderId)
            => await _dataContext.Orders
                .Include(nameof(OrderDetails))
                .FirstOrDefaultAsync(param => param.OrderId.Equals(orderId));

        public async Task<IEnumerable<Order>> GetOrdersAsync()
            => await _dataContext.Orders.ToListAsync();

        public void RegisterOrder(Order order)
            => _dataContext.Add(order);

        public async Task RegisterOrderAsync(Order order)
            => await _dataContext.Orders.AddAsync(order);

        public void SaveChanges()
            => _dataContext.SaveChanges();

        public async Task SaveChangesAsync()
            => await _dataContext.SaveChangesAsync();

        public void UpdateOrder(Order order)
            => _dataContext.Entry(order).State = EntityState.Modified;
    }
}