using System;
using Microsoft.EntityFrameworkCore;
using StockAppWebApi.Models;

namespace StockAppWebApi.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly StockAppContext _context;

        public QuoteRepository(StockAppContext context)
        {
            _context = context;
        }

        public async Task<List<Quote>> GetHistoricalQuotes(int days, int stockId)
        {
            var fromDate = DateTime.Now.Date.AddDays(-days);
            var toDate = DateTime.Now.Date;
            var historicalQuotes = await _context.Quotes
            .Where(q => q.TimeStamp >= fromDate && q.TimeStamp <= toDate
                && q.StockId == stockId) // Kiếm tra stock_id
            .GroupBy(q => q.TimeStamp.Date) // Nhóm theo ngày
            .Select(g => new Quote
            {
                TimeStamp = g.Key,
                Price = g.Average(q => q.Price), // Lấy giá trị trung bình của cùng một ngày
                //dung ve do thi gia theo ngay
            // Các thuộc tính khác của Quote nếu cần thiết
            })
            .OrderBy(q => q.TimeStamp) // Sắp xếp theo thứ tự tăng dần về ngày tháng
            .ToListAsync();
            return historicalQuotes;
        }

        public async Task<List<RealtimeQuote>?> GetRealtimeQuotes(
            int page, int limit, string sector, string industry)
        {
            var query = _context.RealtimeQuotes
                .Skip((page - 1) * limit) //bo qua so luong ban ghi truoc trang hien tai
                .Take(limit); //lay so luong ban ghi toi da tren moi trang
            if (!string.IsNullOrEmpty(sector))
                query = query.Where(q => (q.Sector ?? "").ToLower().Equals(sector.ToLower()));

            if (!string.IsNullOrEmpty(industry))
                query = query.Where(q => q.Industry == industry);

            var quotes = await query.ToListAsync();
            return quotes;
        }
    }
}

