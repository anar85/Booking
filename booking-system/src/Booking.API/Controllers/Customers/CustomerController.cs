using Booking.Application.Common.Pagination;
using Booking.Application.Interfaces.Services.Customers;
using Booking.Application.Interfaces.Services.Helper;
using Booking.Application.Models.DTOs.Files;
using Booking.Application.Models.DTOs.Request.Customers;
using Booking.Application.Models.DTOs.Response.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers.Customers
{
    [Route("/api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ITokenHeaderService _tokenHeaderService;
        public CustomerController(ICustomerService CustomerService, ITokenHeaderService tokenHeaderService)
        {
            _customerService = CustomerService;
            _tokenHeaderService = tokenHeaderService;
        }
        /// <summary>
        /// Get all Customer list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpGet("admin")]
        [ProducesResponseType(typeof(PaginatedList<CustomerResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromHeader(Name = "Accept-Language")] string language, [FromQuery] QueryStringParameters queryParams) => Ok(await _customerService.GetAll(queryParams, language));

        /// <summary>
        /// Get Customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("admin/{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id) => Ok(await _customerService.GetById(id));


        /// <summary>
        /// Get Customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfo()
        {
            var response = await _tokenHeaderService.ParseToken();
            return Ok(await _customerService.GetById(response.CustomerId));
        }

        /// <summary>
        /// Create Customer 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] CustomerRequest request)
        {
            await _customerService.Create(request);
            return StatusCode(201);
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            await _customerService.Update(request);
            return Ok();
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _customerService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Update Customer image
        /// </summary>
        /// <param name="imageRequest"></param>
        /// <returns></returns>
        [HttpPut, Route("photo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePhoto([FromForm] ImageRequest imageRequest)
        {
            await _customerService.ChangeImage(imageRequest);
            return Ok();
        }
    }
}
