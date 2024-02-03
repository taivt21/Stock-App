using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StockAppWebApi.Filters
{
    public class JwtAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _config;
        public JwtAuthorizeFilter(IConfiguration config)
        {
            _config = config;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                //context.Result = new JsonResult(new { message = "Unauthorize" }) { StatusCode = 401 };
                context.Result = new UnauthorizedResult();

                return;
            }
            var tokenHandeler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:SecretKey") ?? "");
            try
            {
               
                tokenHandeler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidateLifetime = true,
                    //RequireExpirationTime = true,
                    //neu token het han thi goi phuong thhuc validateToken
                    //ma loi SecurityTokenExpireExeption se dc throw
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken) ;

                var jwtToken = (JwtSecurityToken)validatedToken;
                if(jwtToken.ValidTo < DateTime.UtcNow)
                {
                    context.Result = new JsonResult(new { message = "UnauthorizedResult" });
                    return;
                }
                var userId = int.Parse(jwtToken.Claims.First().Value);
                context.HttpContext.Items["UserId"] = userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during token validation: {ex.Message}");
                context.Result = new JsonResult(new { message = "UnauthorizedResult" });
                return;
            }
        }
    }
}

