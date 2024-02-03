using System;
using StockAppWebApi.Models;
using StockAppWebApi.Repositories;
using StockAppWebApi.ViewModels;

namespace StockAppWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrder(OrderViewModel orderViewModel)
        {
            if (orderViewModel.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be > 0");
            }

            // Call methods on the actual repository (IOrderRepository), not on the service itself
            return await _orderRepository.CreateOrder(orderViewModel);
        }

        public Task<List<Order>> GetOrderHistory(int userId)
        {
            throw new NotImplementedException();
        }
    }

}

