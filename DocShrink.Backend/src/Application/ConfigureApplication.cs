using Application.Services;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFileProcessingService, FileProcessingService>();
        services.AddSingleton<ISanitizationService, SanitizationService>();
        return services;
    }
}