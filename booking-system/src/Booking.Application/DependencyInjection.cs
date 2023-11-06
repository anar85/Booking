using Booking.Application.Models.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;
using Taxi.Dictionary.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace Booking.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationLayer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.Scan(scan => scan.FromAssemblies(typeof(IApplicationAssemblyMarker).Assembly)
               .AddClasses(@class =>
                     @class.Where(type =>
                           !type.Name.StartsWith('I')
                           && type.Name.EndsWith("Service")
                            )
                  ).AsSelfWithInterfaces().WithScopedLifetime());

            // AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<FileSetting>(configuration.GetSection("FileSetting").Bind);
            services.Configure<TimeSetting>(configuration.GetSection("TimeSetting").Bind);
            // Jwt
            var appSettingsSection = configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<TokenSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
