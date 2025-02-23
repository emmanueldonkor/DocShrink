namespace API.Extensions;

public static class ConfigureCors
{
    public static IServiceCollection RegisterCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("DocShrinkPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();

        }));
        return services;
    }
}