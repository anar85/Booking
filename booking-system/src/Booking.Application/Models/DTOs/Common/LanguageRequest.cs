using System.ComponentModel.DataAnnotations;

namespace Booking.Application.Models.DTOs.Common
{
    public class LanguageRequest
    {
        public int Id { get; set; }

        [Required]
        public string? ShortName { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}
