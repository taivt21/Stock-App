using System;
using Microsoft.AspNetCore.Mvc;
using static StockAppWebApi.Controllers.OrderController;
using StockAppWebApi.Attributes;
using StockAppWebApi.Services;
using StockAppWebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using StockAppWebApi.Extensions;
using StockAppWebApi.Models;

namespace StockAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController(
        IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpPost("place_order")]
        [JwtAuthorize]
        public async Task<IActionResult> CreateOrder(OrderViewModel orderViewModel)
        {
            // Lay userid Tu context
            int userId = HttpContext.GetUserId();
            // Kiếm tra người dùng và cố phiếu tồn tại
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound("User not found.");

            }
            orderViewModel.UserId = userId;
            var createOrder = await _orderService.CreateOrder(orderViewModel);
            return Ok(createOrder);
        }
    }
}
