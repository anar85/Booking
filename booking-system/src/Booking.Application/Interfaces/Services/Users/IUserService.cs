using Booking.Application.Common.Pagination;
using Booking.Application.Models.DTOs.Files;
using Booking.Application.Models.DTOs.Request.Users;
using Booking.Application.Models.DTOs.Response.Users;

namespace Booking.Application.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<PaginatedList<UserResponse>> GetAll(QueryStringParameters queryParamss);
        Task<UserResponse> GetById(int id);
        Task<UserResponse> Create(UserRequest request);
        Task Update(UserRequest request);
        Task Delete(int id);
        Task<GetAccessTokenResponse> GetAccessToken(GetAccessTokenRequest request);
    }
}
