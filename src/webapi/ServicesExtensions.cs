using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace webapi;

public static class ServicesExtensions
{
    public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var origins = configuration["AccessControlAllowOrigins"];
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    if (origins == "*")
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                    else
                    {
                        builder
                            .WithOrigins(origins);
                    }
                });
        });
    }
}