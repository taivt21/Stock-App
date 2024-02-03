using System;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using StockAppWebApi.Models;
using StockAppWebApi.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockAppWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StockAppContext _context;
        private readonly IConfiguration _config;
        public UserRepository(StockAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<User?> Create(RegisterViewModel registerViewModel)
        {
            //đoạn này gọi 1 procedure trong SQL
            //_context.Users.Add(registerViewModel);
            //await _context.SaveChangesAsync();
            string sql = "EXEC dbo.RegisterUser @username, @password, @email, @phone, @full_name, @date_of_birth, @country";

            IEnumerable<User> result = await _context.Users.FromSqlRaw(sql,
                                    new SqlParameter("@username", registerViewModel.Username ?? ""),
                                    new SqlParameter("@password", registerViewModel.Password),
                                    new SqlParameter("@email", registerViewModel.Email),
                                    new SqlParameter("@phone", registerViewModel.Phone ?? ""),
                                    new SqlParameter("@full_name", registerViewModel.FullName ?? ""),
                                    new SqlParameter("@date_of_birth", registerViewModel.DateOfBirth),
                                    new SqlParameter("@country", registerViewModel.Country)).ToListAsync();

            User? user = result.FirstOrDefault();
            return user;
            //try
            //{
            //    IEnumerable<User> result = await _context.Users.FromSqlRaw(sql,
            //        new SqlParameter("@username", registerViewModel.Username ?? ""),
            //        new SqlParameter("@password", registerViewModel.Password),
            //        new SqlParameter("@email", registerViewModel.Email),
            //        new SqlParameter("@phone", registerViewModel.Phone ?? ""),
            //        new SqlParameter("@full_name", registerViewModel.FullName ?? ""),
            //        new SqlParameter("@date_of_birth", registerViewModel.DateOfBirth),
            //        new SqlParameter("@country", registerViewModel.Country)).ToListAsync();

            //    User? user = result.FirstOrDefault();
            //    return user;
            //}
            //catch (Exception ex)
            //{
            //    // Xử lý lỗi theo nhu cầu của bạn
            //    Console.WriteLine($"Error executing stored procedure: {ex.Message}");
            //    return null;
            //}
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            string sql = "EXEC dbo.CheckLogin @email, @password";

            IEnumerable<User> result = await _context.Users.FromSqlRaw(sql,
                                        new SqlParameter("@email", loginViewModel.Email),
                                        new SqlParameter("@password", loginViewModel.Password)).ToListAsync();

            User? user = result.FirstOrDefault();
            if (user != null)
            {
                // Kiểm tra mật khẩu

                // Tạo JWT token nếu xác thực thành công
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"] ?? "");


                // Tạo mô tả cho JWT
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    // Thông tin được chứa trong phần subject của JWT
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    // Thêm thông tin cần thiết vào payload của JWT
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        // Thêm các claim khác nếu cần
                    }),

                    // Thời điểm hết hạn của JWT (ở đây là 30 ngày)
                    Expires = DateTime.UtcNow.AddDays(30),

                    // Key và thuật toán sử dụng để ký JWT
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                // Tạo JWT token
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // Chuyển đổi JWT token thành chuỗi
                var jwtToken = tokenHandler.WriteToken(token);


                return jwtToken;
            }
            else
            {
                throw new Exception("Wrong email or password");

            }
            
        }

    }
}

