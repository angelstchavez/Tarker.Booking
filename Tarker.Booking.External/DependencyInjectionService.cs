using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tarker.Booking.Application.External.GetTokenJWT;
using Tarker.Booking.External.GetTokenJWT;

namespace Tarker.Booking.External
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IGetTokenJWTService, GetTokenJWTService>();
            return services;
        }
    }
}
