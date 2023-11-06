using Booking.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Entities.Users
{
    public class Role : BaseEntity<int>
    {
        [Required]
        public string? Name { get; set; }
    }
}
