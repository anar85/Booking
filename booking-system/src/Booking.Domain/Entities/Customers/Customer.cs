using Booking.Domain.Entities.Common;
using Booking.Domain.Entities.Orders;
using Booking.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Entities.Customers
{
    public class Customer:BaseEntity<int>
    {
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
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
        [Required]
        public int GenderId { get; set; }
        public string? Description { get; set; }
        public int LanguageId { get; set; } = 1;
        public Order? Order { get; set; }
    }
}
