using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MediatRCqrs.Configuration.SwaggerConfiguration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "People API",
                    Description = "A simple example ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Babak Taremi",
                        Email = string.Empty,
                    },
                    
                });
            });
        }
    }
}
