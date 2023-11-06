using Microsoft.AspNetCore.Http;

namespace Booking.Application.Models.DTOs.Files
{
    public class FileRequest
    {
        public string? FilePath { get; set; }
        public IFormFile? File { get; set; }

    }
}
