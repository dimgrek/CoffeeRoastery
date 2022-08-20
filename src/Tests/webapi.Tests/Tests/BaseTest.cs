using System;
using System.Net.Http;
using CoffeeRoastery.DAL.PostgreSQL.Context;
using Microsoft.Extensions.DependencyInjection;
using webapi.Tests.Auth;
using webapi.Tests.Utilities;
using Xunit;

namespace webapi.Tests.Tests;

[Collection("Test server")]
public abstract class BaseTest : IDisposable
{
    private bool disposedValue;
    protected readonly HttpClient Client;
    protected readonly CoffeeRoasteryContext Context;
    protected readonly IServiceScope ServiceScope;

    protected BaseTest(CustomWebApplicationFactory<Startup> factory)
    {
        TestAuthHandler.ResetUser();
        Client = factory.CreateAuthClient();
        ServiceScope = factory.Services.CreateScope();
        Context = ServiceScope.ServiceProvider.GetRequiredService<CoffeeRoasteryContext>();
    }

    protected virtual void Dispose(bool disposing)
    {
        Setup.ClearDb(Context);
        if (!disposedValue)
        {
            if (disposing)
            {
                ServiceScope.Dispose();
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}