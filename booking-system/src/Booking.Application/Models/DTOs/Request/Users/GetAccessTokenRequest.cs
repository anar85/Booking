namespace Booking.Application.Models.DTOs.Request.Users
{
    public class GetAccessTokenRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
