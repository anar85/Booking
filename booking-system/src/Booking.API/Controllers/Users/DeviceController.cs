using Booking.Application.Common.Pagination;
using Booking.Application.Interfaces.Services.Helper;
using Booking.Application.Interfaces.Services.Users;
using Booking.Application.Models.DTOs.Request.Users;
using Booking.Application.Models.DTOs.Response.Users;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers.Users
{
    [Route("/api/user/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IUserDeviceService _userDeviceService;
        private readonly ITokenHeaderService _tokenHeaderService;

        public DeviceController(IUserDeviceService userDeviceService, ITokenHeaderService tokenHeaderService)
        {
            _userDeviceService = userDeviceService;
            _tokenHeaderService = tokenHeaderService;
        }


        /// <summary>
        /// Check user deviceid 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] DeviceRequest request)
        {
            var response = await _tokenHeaderService.ParseToken();
           request.UserId= response.UserId;
            await _userDeviceService.CheckDevice(request);
            return StatusCode(201);
        }


        /// <summary>
        /// Remove user deviceid 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromBody] DeviceRequest request)
        {
            var response = await _tokenHeaderService.ParseToken();
            request.UserId = response.UserId;
            await _userDeviceService.RemoveDevice(request);
            return Ok();
        }

    }
}
