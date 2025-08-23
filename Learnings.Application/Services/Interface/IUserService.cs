using Learnings.Application.Dtos;
using Learnings.Application.ResponseBase;
using Learnings.Domain.Entities;

namespace Learnings.Application.Services.Interface
{
    public interface IUserService
    {
        Task<ResponseBase<UserDto>> GetUserByIdAsync(Guid id);
        Task<ResponseBase<List<UsersDto>>> GetAllUsersAsync();
        Task<ResponseBase<List<Users>>> GetAllUsersAsyncIdentity();
        Task<ResponseBase<List<UsersDto>>> GetAllAdminsAsync();
        Task<ResponseBase<UserDto>> AddUserAsync(UserDto user);
        Task<ResponseBase<Users>> AddUserAsyncIdentity(UserDto user);
        Task<ResponseBase<Users>> CheckEmailExists(string email);
        Task<ResponseBase<UserDto>> UpdateUserAsync(int id,UserDto user);
        Task<ResponseBase<UserDto>> DeleteUserAsync(int id);
        Task<TokenResponse> LoginAsync(LoginDto loginRequest);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken);
        Task<ResponseBase<Users>> ChangePassword(ChangePasswordModel model);
        Task<ResponseBase<Users>> ChangeForgetPassword(ResetPassword model);
        Task<ResponseBase<Users>> ForgetPassword(CheckDuplicateUser model);
    }
}
