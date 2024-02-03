using System;
using StockAppWebApi.Models;
using StockAppWebApi.Repositories;

namespace StockAppWebApi.Services
{
    public class CWService : ICWService
    {
        private readonly ICWRepository _iCWRepository;
        public CWService(ICWRepository iCWRepository)
        {
            _iCWRepository = iCWRepository;
        }

        public async Task<List<CoveredWarrant>> GetCWByStockId(int stockId)
        {
            return await _iCWRepository.GetCWByStockId(stockId);
        }
    }
}

