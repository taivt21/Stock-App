using System;
using Microsoft.EntityFrameworkCore;
using StockAppWebApi.Models;

namespace StockAppWebApi.Repositories
{
    public class CWRepository : ICWRepository
    {
        private readonly StockAppContext _context;

        public CWRepository(StockAppContext context)
        {
            _context = context;
        }

        public async Task<List<CoveredWarrant>> GetCWByStockId(int stockId)
        {
            return await _context.CoveredWarrants
                .Where(cw => cw.UnderlyingAssetId == stockId)
                .Include(cw => cw.Stock)
                .ToListAsync();
        }
    }
}

