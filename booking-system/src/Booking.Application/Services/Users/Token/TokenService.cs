using Booking.Application.Common.Jwt.Enums;
using Booking.Application.Common.Jwt.Models;
using Booking.Application.Exceptions;
using Booking.Application.Interfaces.Services.Helper;
using Booking.Application.Interfaces.Services.Token;
using Booking.Application.Models.Configs;
using Booking.Application.Models.Constants;
using Booking.Application.Models.DTOs.Response.Users.Token;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.Application.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _appSettings;
        private readonly ITokenResponseService _tokenResponseService;
        public TokenService(IOptions<TokenSettings> appSettings, ITokenResponseService tokenResponseService)
        {
            _appSettings = appSettings.Value;
            _tokenResponseService = tokenResponseService;
        }

        public async Task<TokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            var ttlToken = request.TokenType == TokenType.Access ? _appSettings.TtlAccessToken : _appSettings.TtlRefreshToken;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var response = await _tokenResponseService.GetTokenInfoByUserId(request.UserId);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, request.UserId.ToString()),
                     new Claim("ProviderId", response.ProviderId.ToString()),
                    new Claim("CustomerId",response.CustomerId.ToString()),
                    new Claim("RoleId", request.RoleId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddSeconds(ttlToken),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return new TokenResponse{
              Token= tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
              IsProvider = response.ProviderId>0?true:false
            };
        }

        public VerifyTokenResponse Verify(string token)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var claims = handler.ValidateToken(token.Replace("Bearer ","").Replace("bearer ", ""), validations, out var tokenSecure);
                return new VerifyTokenResponse
                {
                    UserId = Convert.ToInt32(claims.Identity.Name.ToString()),
                    ProviderId = Convert.ToInt32(claims.FindFirst(claim => claim.Type == "ProviderId").Value),
                    CustomerId = Convert.ToInt32(claims.FindFirst(claim => claim.Type == "CustomerId").Value),
                    RoleId = Convert.ToInt32(claims.FindFirst(claim => claim.Type == "RoleId").Value),
                    isNotAdmin = Convert.ToInt32(claims.FindFirst(claim => claim.Type == "RoleId").Value) > 1 ? true : false
                };
            }
            catch
            {
                throw new UnauthorizedException(ExceptionCodes.UnauthorizedError, $"User is unauthorized");
            }
        }
    }
}
