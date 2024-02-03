using System;
using Microsoft.AspNetCore.Mvc;
using StockAppWebApi.Models;
using StockAppWebApi.Services;

namespace StockAppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoveredWarrantController : ControllerBase
    {
        private readonly ICWService _cwService;
        public CoveredWarrantController(ICWService cwService)
        {
            _cwService
            = cwService;
        }


        [HttpGet("stock/{stockId}")]
        public async Task<IActionResult> GetCWByStockId(int stockId)
        {
            try
            {
                //SELECT * FROM covered_warrants WHERE underlying_asset_id=2;
                var coveredWarrants = await _cwService.GetCWByStockId(stockId);
                return Ok(coveredWarrants);
            }

            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred: {ex.Message}");

            }
        }

    }
}

