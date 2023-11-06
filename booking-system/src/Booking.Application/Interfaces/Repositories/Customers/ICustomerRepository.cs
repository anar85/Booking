using Booking.Domain.Entities.Customers;

namespace Booking.Application.Interfaces.Repositories.Customers
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
    }
}
