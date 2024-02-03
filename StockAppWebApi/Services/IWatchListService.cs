using System;
using StockAppWebApi.Models;
using StockAppWebApi.ViewModels;

namespace StockAppWebApi.Services
{
	public interface IWatchListService
	{
        Task AddStockToWatchList(int userId,int stockId);
        Task<WatchList?> GetWatchlist(int userId, int stockId);
    }
}

