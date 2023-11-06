using System.ComponentModel.DataAnnotations;

namespace Booking.Application.Models.DTOs.Request.Users
{
    public class UserRequest
    {
        public int Id { get; set; } = 0;

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
        public bool Noname { get; set; } = false;
    }
}
