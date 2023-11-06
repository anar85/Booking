using Booking.Application.Interfaces.Repositories.Users;
using Booking.Domain.Entities.Users;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Repositories;

namespace Booking.Infrastucture.Repositories.Customers
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(BookingDbContext context) : base(context)
        {

        }
    }
}
