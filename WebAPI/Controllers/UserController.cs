﻿using Bussines.DTO;
using Bussines.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using DataAccess.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediaService _mediaService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IMediaService mediaService, IAuthService authService)
        {
            _userService = userService;
            _mediaService = mediaService;
            _authService = authService;
        }
        [HttpPost("/user")]
        public async Task<ApiResponse> AddUser([FromBody] RegisterUserDto userDto)
        {
            var result = await _userService.SaveUser(userDto);
            return result;
        }

        [HttpGet("/user-profile")]
        [Authorize]
        public async Task<UserDto> GetUser()
        {
            if (_authService.IsTokenExpired(HttpContext.User))
            {
                // Token süresi geçmiş, hata yanıtı dönme
                Response.StatusCode = 401; // Unauthorized
                return null;
            }
            var user =  _userService.FindLoginUser(HttpContext.User);
            return user;
        }

        [HttpPut("/user-update")]
        [Authorize]
        public async Task<bool> UpdateUser([FromForm] UserDto userDto)
        {
            var result =await _userService.UserUpdate(userDto, HttpContext.User);
            return result;
        }

        [HttpGet("/all-user")]
        public async Task<List<UserDto>> GetAllUser()
        {
            var result = await _userService.GetAllUser();
            return result;
        }

        [HttpGet("/check-notify")]
        [Authorize]
        public async Task<ApiResponse> GetNotify()
        {
            var result = await _userService.CheckNotify(HttpContext.User);
            return result;
        }
        [HttpPost("/notify-handler")]
        [Authorize]
        public async Task<ApiResponse> NotifyHandler([FromBody] RequestDto requestDto)
        {
            var result = await _userService.NotifyRequestHandler(requestDto);
            return result;
        }

        [HttpGet("/get-team-member/{teamId}")]
        public async Task<List<UserDto>> NotTeamMember(int teamId)
        {
            var result = await _userService.NotTeamMember(teamId, HttpContext.User);
            return result;
        }

        [HttpPost("/change-password")]
        [Authorize]
        public async Task<ApiResponse> ChangePassword([FromBody] PasswordDto passwordDto)
        {
            var result = await _userService.ChangePassword(passwordDto, HttpContext.User);
            return result;
        }
    }
}
