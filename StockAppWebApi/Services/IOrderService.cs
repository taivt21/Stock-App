using System;
using StockAppWebApi.Models;
using StockAppWebApi.ViewModels;

namespace StockAppWebApi.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(OrderViewModel orderViewModel);
        Task<List<Order>> GetOrderHistory(int userId);
    }
}

