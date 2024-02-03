using System;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace StockAppWebApi.Controllers
{

    [Route("api/ws")]
	public class WebSocketController: ControllerBase
	{
		[HttpGet]
		public async Task Get()
		{
			if (HttpContext.WebSockets.IsWebSocketRequest)
			{
				using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
				//sinh ngau nhien 2 gia tri x,y thay doi 2 giay
				var random = new Random();
				while(webSocket.State == WebSocketState.Open)
				{
					//tao gia tri x,y ngau nhien
					int x = random.Next(1, 100);
					int y = random.Next(1, 100);
					var buffer = Encoding.UTF8.GetBytes($"{{ \"x\": {x}, \"y\": {y} }}");
					await webSocket.SendAsync(
						new ArraySegment<byte>(buffer),
						WebSocketMessageType.Text, true, CancellationToken.None);
					await Task.Delay(2000);
                }
				await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
					"Connect closed by the server", CancellationToken.None);
			}
			else
			{
				HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
			}
		}
	}
}

