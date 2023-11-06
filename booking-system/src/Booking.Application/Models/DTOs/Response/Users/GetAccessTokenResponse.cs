namespace Booking.Application.Models.DTOs.Response.Users
{
    public class GetAccessTokenResponse
    {
        public int UserId { get; set; }
        public string? AccessToken { get; set; }
        public bool IsProvider { get; set; }
    }
}
