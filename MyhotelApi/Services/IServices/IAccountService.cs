﻿using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Views;
using MyhotelApi.Objects.Models;

namespace MyhotelApi.Services.IServices;

public interface IAccountService
{
    ValueTask<UserView> AddUserAsync(SignInUserDto signInUserDto);
    ValueTask<List<UserView>> GetUsersAsync();
    ValueTask<UserView> GetUserByIdAsync(Guid userId);
    ValueTask<UserView> UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto);
    ValueTask DeleteUserAsync(Guid userId, bool fullyDelete = false);
    ValueTask<User?> CheckEmailExistAsync(string email);
}