using System;
namespace StockAppWebApi.Extensions
{
	public static class HttpContextExtensions
	{
		//public HttpContextExtensions()
		//{
		//}
		public static int GetUserId(this HttpContext httpContext)
		{
			return httpContext.Items["UserId"] as int? ??
					throw new Exception("UserId not found in httpContext.Items");
		}
	}
}

