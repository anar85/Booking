namespace Booking.Application.Common.Jwt.Models
{
    public class VerifyTokenResponse
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int ProviderId { get; set; } = 0;
        public int CustomerId { get; set; } = 0;
        public bool isNotAdmin { get; set; }
    }
}
