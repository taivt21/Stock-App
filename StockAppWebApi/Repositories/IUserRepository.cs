﻿using System;
using StockAppWebApi.Models;
using StockAppWebApi.ViewModels;

namespace StockAppWebApi.Repositories
{
	public interface IUserRepository
	{
        Task<User?> GetByEmail(string email);
        Task<User?> GetById(int id);
        Task<User?> GetByUsername(string username);
        Task<string> Login(LoginViewModel loginViewModel);
        Task<User?> Create(RegisterViewModel registerViewModel);
    }
}

