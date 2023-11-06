using Booking.Application.Interfaces.Services.Users;
using Booking.Application.Models.DTOs.Request.Users;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers.Users
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)=>_userService = userService;
        
        /// <summary>
        /// Get Access Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccessToken([FromBody] GetAccessTokenRequest request) => Ok(await _userService.GetAccessToken(request));
    }
}
