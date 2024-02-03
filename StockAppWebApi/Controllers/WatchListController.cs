﻿using System;
using Microsoft.AspNetCore.Mvc;
using StockAppWebApi.Attributes;
using StockAppWebApi.Extensions;
using StockAppWebApi.Services;

namespace StockAppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListService _watchlistService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;

        public WatchListController(IWatchListService watchlistService, IUserService userService, IStockService stockService)
        {
            _watchlistService = watchlistService;
            _userService = userService;
            _stockService = stockService;
        }

        [HttpPost("AddStockToWatchList/{stockId}")]
        [JwtAuthorize]
        public async Task<IActionResult> AddStockToWatchList(int stockId)
        {
            //lay useId tu context.HttpContext.Items["UserId"] = userId (trong filter)
            int userId = HttpContext.GetUserId();

            // Kiểm tra người dùng và cổ phiếu tồn tại
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var stock = await _stockService.GetStockById(stockId);

            if (stock == null)
            {
                return NotFound("Stock not found.");
            }

            // Kiểm tra xem cố phiếu đã tồn tại trong watchlist của người dùng chưa
            var existingWatchlistItem = await _watchlistService
                .GetWatchlist(userId, stockId);
            if (existingWatchlistItem != null)
            {
                return BadRequest("Stock is already in watchlist.");
            }
        
            await _watchlistService.AddStockToWatchList(userId, stockId);
            return Ok();
    }
    }
}

