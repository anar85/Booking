using AutoMapper;
using Booking.Application.Models.DTOs.Request.Users;
using Booking.Application.Models.DTOs.Response.Users;
using Booking.Domain.Entities.Users;

namespace Booking.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
