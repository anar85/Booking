using Booking.Domain.Entities.Common;
using Booking.Domain.Entities.Customers;
using Booking.Domain.Entities.Providers;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Entities.Users
{
    public class User : BaseEntity<int>
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public int? RoleId { get; set; }

        public Role? Role { get; set; }
        public bool Noname { get; set; }=false;
        public Customer? Customer { get; set; }
        public Provider? Provider { get; set; }
    }
}
