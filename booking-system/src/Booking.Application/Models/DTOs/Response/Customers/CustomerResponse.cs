using Booking.Application.Models.DTOs.Common;
using Booking.Application.Models.DTOs.Files;

namespace Booking.Application.Models.DTOs.Response.Customers
{
    public class CustomerResponse : BaseResponse<int>
    {
        public FileResponse? Image { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? MiddleName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? Description { get; set; }
        public int LanguageId { get; set; }
    }
}
