using Booking.Application.Models.DTOs.Request.Users;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Booking.Application.Models.DTOs.Request.Customers
{
    public class CustomerRequest
    {
        public int Id { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? MiddleName { get; set; }
        public string? ProfileImageUrl { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Password { get; set; }
        [Required]
        public int GenderId { get; set; }
        public string? Description { get; set; }
        public int LanguageId { get; set; }
        public bool Noname { get; set; } = false;
        public UserRequest? User { get; set; } 
    }
}
