using System;
using StockAppWebApi.Models;

namespace StockAppWebApi.Services
{
    public interface ICWService
    {
        Task<List<CoveredWarrant>> GetCWByStockId(int stockId);
    }
}

