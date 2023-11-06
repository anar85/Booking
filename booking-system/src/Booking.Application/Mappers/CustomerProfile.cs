using AutoMapper;
using Booking.Application.Models.DTOs.Request.Customers;
using Booking.Application.Models.DTOs.Response.Customers;
using Booking.Domain.Entities.Customers;

namespace Booking.Application.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }
    }
}
