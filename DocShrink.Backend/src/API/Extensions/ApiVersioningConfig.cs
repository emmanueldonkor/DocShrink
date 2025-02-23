using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;

public static class ApiVersioningConfig
{
    public static IServiceCollection ConfigureApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true; 
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0); 
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; 
            });

            return services;
    }
}