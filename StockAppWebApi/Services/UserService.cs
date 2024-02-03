using System;
using StockAppWebApi.Models;
using StockAppWebApi.Repositories;
using StockAppWebApi.ViewModels;

namespace StockAppWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }


        public async Task<User?> Register(RegisterViewModel registerViewModel)
        {
            // Perform validation or additional logic if needed before registering the user
            //User user = new User
            //{
            //    Username = registerViewModel.Username ?? ""
            //};
            // Check if the username or email is already taken
            // tao doi tuong User tu RegisterViewModel

            var existingUserByUsername = await _userRepository.GetByUsername(registerViewModel.Username ?? "");
            var existingUserByEmail = await _userRepository.GetByEmail(registerViewModel.Email);

            if (existingUserByUsername != null)
            {
                // Username is already taken
                throw new ArgumentException("Username already exists");
            }

            if (existingUserByEmail != null)
            {
                // Email is already taken
                //return -2;
                throw new ArgumentException("Email already exists");

            }

            // Register the user
            return await _userRepository.Create(registerViewModel);
        }


        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            return await _userRepository.Login(loginViewModel);
        }

        public async Task<User?> GetUserById(int userId)
        {
            User? user = await _userRepository.GetById(userId);
            return user;
        }
    }
}

