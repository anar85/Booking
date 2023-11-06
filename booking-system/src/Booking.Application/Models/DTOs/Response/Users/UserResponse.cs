using Booking.Application.Models.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace Booking.Application.Models.DTOs.Response.Users
{
    public class UserResponse : BaseResponse<int>
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public int? RoleId { get; set; }
    }
}
