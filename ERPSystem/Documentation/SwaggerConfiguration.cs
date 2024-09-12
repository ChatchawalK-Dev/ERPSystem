using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ERPSystem.Documentation
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP System", Version = "v1" });
            });
        }
    }
}
