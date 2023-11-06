using Booking.Application.Common.Pagination;
using Booking.Application.Models.DTOs.Files;
using Booking.Application.Models.DTOs.Request.Customers;
using Booking.Application.Models.DTOs.Response.Customers;

namespace Booking.Application.Interfaces.Services.Customers
{
    public interface ICustomerService
    {
        Task<PaginatedList<CustomerResponse>> GetAll(QueryStringParameters queryParams, string lang);
        Task<CustomerResponse> GetById(int id);
        Task<int> Create(CustomerRequest request);
        Task Update(CustomerRequest request);
        Task Delete(int id);
        Task ChangeImage(ImageRequest request);
    }
}
