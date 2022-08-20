using Xunit;

namespace webapi.Tests;

[CollectionDefinition("Test server")]
public class TestServerCollection : ICollectionFixture<CustomWebApplicationFactory<Startup>>
{
}