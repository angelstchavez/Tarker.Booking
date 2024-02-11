using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Tarker.Booking.Api
{
    /// <summary>
    /// Service for configuring dependency injection related to the Web API.
    /// </summary>
    public static class DependencyInjectionService
    {
        /// <summary>
        /// Adds necessary services for the Web API.
        /// </summary>
        /// <param name="services">The collection of services to add to.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tarker Booking API",
                    Description = "API para gestionar reservas en el sistema Tarker.",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipo de desarrollo de Tarker",
                        Email = "contacto@tarker.com",
                        Url = new Uri("https://www.tarker.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licencia MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]
                        {

                        }
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
