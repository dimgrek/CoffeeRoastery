using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using CoffeeRoastery.DAL.PostgreSQL.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using webapi.Tests.Auth;
using webapi.Tests.Utilities;

namespace webapi.Tests;

public class CustomWebApplicationFactory<TStartup>
       : WebApplicationFactory<TStartup> where TStartup : class
{
    private const string TestAuthenticateScheme = "TestScheme";
    private readonly Dictionary<string, string> configSettings = new();

    public HttpClient CreateAuthClient()
    {
        var client = WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthenticateScheme;
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        TestAuthenticateScheme, _ => { });
            });
        })
        .CreateClient();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "stub-token");

        return client;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Use InMemoryDb
            UseInMemoryDatabase(services);

            SetupDatabase(services);
        }).ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(configSettings);
        });
    }

    private void UseInMemoryDatabase(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(DbContextOptions<CoffeeRoasteryContext>));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<CoffeeRoasteryContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDbForTesting");
        });
    }

    private void SetupDatabase(IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();

        using var scope = sp.CreateScope();

        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<CoffeeRoasteryContext>();
        var logger = scopedServices
            .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

        try
        {
            db.Database.EnsureDeleted();
            if (db.Database.EnsureCreated())
            {
                Setup.InitializeDbForTests(db);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the " +
                "database with test messages. Error: {Message}", ex.Message);

            throw;
        }
    }
}