namespace Booking.Application.Models.Configs
{
    public class TokenSettings
    {
        public string? Secret { get; set; }
        public int TtlAccessToken { get; set; }
        public int TtlRefreshToken { get; set; }
    }
}
