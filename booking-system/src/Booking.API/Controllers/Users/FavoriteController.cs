using Booking.Application.Common.Pagination;
using Booking.Application.Interfaces.Services.Helper;
using Booking.Application.Interfaces.Services.Users;
using Booking.Application.Models.DTOs.Request.Users;
using Booking.Application.Models.DTOs.Response.Users;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers.Users
{
    [Route("/api/favotites")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IUserFavoriteService _userFavoriteService;
        private readonly ITokenHeaderService _tokenHeaderService;
        public FavoriteController(IUserFavoriteService userFavoriteService, ITokenHeaderService tokenHeaderService)
        {
            _userFavoriteService = userFavoriteService;
            _tokenHeaderService = tokenHeaderService;
        }


        /// <summary>
        /// Create User 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] FavoriteRequest request)
        {
            var response = await _tokenHeaderService.ParseToken();
            request.UserId = response.UserId;
            await _userFavoriteService.Create(request);
            return StatusCode(201);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] FavoriteRequest request)
        {
            var response = await _tokenHeaderService.ParseToken();
            request.UserId = response.UserId;
            await _userFavoriteService.Delete(request);
            return Ok();
        }

    }
}
