namespace Booking.Application.Models.DTOs.Common
{
    public class LanguageResponse : BaseResponse<int>
    {
        public string? ShortName { get; set; }
        public string? Name { get; set; }
    }
}
