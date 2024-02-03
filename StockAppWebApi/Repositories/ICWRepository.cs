using System;
using StockAppWebApi.Models;

namespace StockAppWebApi.Repositories
{
    public interface ICWRepository
    {
        public Task<List<CoveredWarrant>> GetCWByStockId(int stockId);


    }
}

