using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Tarker.Booking.Api
{
    public static class DependencyInjectionService
    {
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
                        Url = new System.Uri("https://www.tarker.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licencia MIT",
                        Url = new System.Uri("https://opensource.org/licenses/MIT")
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
