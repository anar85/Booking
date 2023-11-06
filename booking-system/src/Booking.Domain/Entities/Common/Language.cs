using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Entities.Common
{
    public class Language : BaseEntity<int>
    {
        [Required]
        public string? ShortName { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
