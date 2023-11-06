using Booking.Application.Common.Jwt.Enums;

namespace Booking.Application.Common.Jwt.Models
{
    public class GenerateTokenRequest
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }

        public TokenType TokenType { get; set; }
    }
}
