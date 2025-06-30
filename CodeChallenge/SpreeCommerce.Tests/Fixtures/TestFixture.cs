namespace SpreeCommerce.Tests.Fixtures;

public class TestFixture : IDisposable
{
    public IConfiguration Configuration { get; private set; }
    public ServiceProvider ServiceProvider { get; private set; }

    public TestFixture(IConfiguration configuration)
    {
        ServiceProvider = Startup.Services.BuildServiceProvider();
        Configuration = configuration;
    }

    public void Dispose()
    {
        ServiceProvider.Dispose();
    }

}