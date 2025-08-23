using Learnings.Application.Dtos;
using Learnings.Application.ResponseBase;
using Learnings.Application.Services.Interface;
using Learnings.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Core_Learnings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBase<UserDto>>> GetUserById(Guid id)
        {
            var response = await _userService.GetUserByIdAsync(id);

            if (response.Data == null)
            {
                return NotFound(new ResponseBase<string>(null, "User not found", HttpStatusCode.NotFound));
            }

            return Ok(response);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResponseBase<List<Users>>>> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            if(response.Data == null)
            {
                return NotFound(new ResponseBase<string>(null, "Users not found", HttpStatusCode.NotFound));
            }
            return Ok(response);
        }

        [HttpPost]
        public async  Task<ActionResult<ResponseBase<Users>>> AddUser(UserDto user)
        {
            var response = await _userService.AddUserAsyncIdentity(user);

            return Ok(response);
        }
        [HttpPost("duplicateUser")]
        public async Task<ActionResult<ResponseBase<Users>>> CheckEmail([FromBody] CheckDuplicateUser email)
        {
            var response = await _userService.CheckEmailExists(email.Email);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {

            var response = await _userService.UpdateUserAsync(id,userDto);
            if (response.Data == null)
            {
                return NotFound(new ResponseBase<string>(null, "User not found", HttpStatusCode.NotFound));
            }

            return Ok(response);
        }
        [HttpGet("GetAllAdmins")]
        public async Task<ActionResult<ResponseBase<List<UsersDto>>>> GetAllAdmins()
        {
            var response = await _userService.GetAllAdminsAsync();
            if (response.Data == null)
            {
                return NotFound(new ResponseBase<string>(null, "Users not found", HttpStatusCode.NotFound));
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            if (response.Data == null)
            {
                return NotFound(new ResponseBase<string>(null, "User not found", HttpStatusCode.NotFound));
            }

            return Ok(new ResponseBase<string>(null,"User deleted successfully", HttpStatusCode.OK));
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        //{
        //    if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
        //    {
        //        return BadRequest(new { Message = "Email and password are required." });
        //    }

        //    var response = await _userService.LoginAsync(loginRequest);

        //    if (response == null)
        //    {
        //        return Unauthorized(new { Message = "Invalid email or password." });
        //    }

        //    return Ok(response);
        //}

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];
        //    if (string.IsNullOrEmpty(refreshToken))
        //        return Unauthorized(new { message = "Refresh token not found in cookies" });

        //    var response = await _userService.RefreshTokenAsync(refreshToken);
        //    if (response == null)
        //        return Unauthorized(new { message = "Invalid refresh token" });

        //    Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true,
        //        SameSite = SameSiteMode.Strict,
        //        Expires = DateTime.UtcNow.AddDays(7)
        //    });

        //    return Ok(new
        //    {
        //        accessToken = response,
        //        expires = response.Expiration
        //    });
        //}
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized(new { message = "Refresh token not found in cookies" });

            var response = await _userService.RefreshTokenAsync(refreshToken);
            if (response == null)
                return Unauthorized(new { message = "Invalid refresh token" });

            return Ok(new
            {
                accessToken = response.Token,
                expires = response.Expiration
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new { Message = "Email and password are required." });
            }

            var response = await _userService.LoginAsync(loginRequest);

            if (response == null)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            return Ok(new
            {
                accessToken = response.Token,
                expires = response.Expiration
            });
        }

        [HttpPost("changeForgetPassword")]
        public async Task<ResponseBase<Users>> ChangeForgetPassword([FromBody] ResetPassword model)
        {
            var response = await _userService.ChangeForgetPassword(model);

            return response;
        }
        [HttpPost("resetPassword")]
        public async Task<ResponseBase<Users>> ResetPassword([FromBody] ChangePasswordModel model)
        {
            var response = await _userService.ChangePassword(model);

            return response;
        }
        [HttpPost("forgetPassword")]
        public async Task<ResponseBase<Users>> ForgetPassword([FromBody] CheckDuplicateUser model)
        {
            var response = await _userService.ForgetPassword(model);

            return response;
        }

    }
}
