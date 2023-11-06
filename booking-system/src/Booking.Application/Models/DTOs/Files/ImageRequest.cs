using Microsoft.AspNetCore.Http;

namespace Booking.Application.Models.DTOs.Files
{
    public class ImageRequest
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
    }
}
