using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Models.DTOs.Response.Users.Token
{
    public class TokenResponse
    {
        public bool IsProvider { get; set; }= false;
        public string? Token { get; set; }
    }
}
