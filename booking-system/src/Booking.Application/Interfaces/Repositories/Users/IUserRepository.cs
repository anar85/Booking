using Booking.Domain.Entities.Providers;
using Booking.Domain.Entities.Users;

namespace Booking.Application.Interfaces.Repositories.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
