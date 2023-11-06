using Booking.Application.Common.Jwt.Models;
using Booking.Application.Models.DTOs.Response.Users.Token;

namespace Booking.Application.Interfaces.Services.Token
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateToken(GenerateTokenRequest request);
        VerifyTokenResponse Verify(string token);
    }
}
