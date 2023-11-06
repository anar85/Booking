using Booking.Application.Interfaces.Repositories.Customers;
using Booking.Domain.Entities.Customers;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Repositories;

namespace Booking.Infrastucture.Repositories.Customers
{
    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(BookingDbContext context) : base(context)
        {

        }
    }
}
