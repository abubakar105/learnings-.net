﻿using Learnings.Application.Dtos;
using Learnings.Application.ResponseBase;
using Learnings.Domain.Entities;

namespace Learnings.Application.Services.Interface
{
    public interface IUserService
    {
        Task<ResponseBase<UserDto>> GetUserByIdAsync(int id);
        Task<ResponseBase<List<UserDto>>> GetAllUsersAsync();
        Task<ResponseBase<List<Users>>> GetAllUsersAsyncIdentity();
        Task<ResponseBase<UserDto>> AddUserAsync(UserDto user);
        Task<ResponseBase<Users>> AddUserAsyncIdentity(UserDto user);
        Task<ResponseBase<UserDto>> UpdateUserAsync(int id,UserDto user);
        Task<ResponseBase<UserDto>> DeleteUserAsync(int id);
        Task<TokenResponse> LoginAsync(LoginDto loginRequest);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken);
        Task<ResponseBase<Users>> ChangePassword(ChangePasswordModel model);
    }
}
