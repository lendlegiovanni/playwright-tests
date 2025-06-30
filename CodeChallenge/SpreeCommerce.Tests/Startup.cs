using Microsoft.Extensions.Hosting;

namespace SpreeCommerce.Tests;

public class Startup
{
    public static IConfiguration Configuration { get; private set; }
    public static IServiceCollection Services { get; private set; }

    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        //Execute only when running locally
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_BUILDID")))
        {
            DotNetEnv.Env.Load();
        }
        var host = hostBuilder
             .ConfigureHostConfiguration(builder => _ = builder
                 .AddJsonFile("appsettings.json")
                 .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("UITARGETENVIRONMENT")}.json", true)
                 .AddUserSecrets<Startup>()
                 .AddEnvironmentVariables());
        host.ConfigureServices((context, services) =>
        {
            Configuration = context.Configuration;
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
            options.SerializerOptions.PropertyNamingPolicy = null;
            options.SerializerOptions.WriteIndented = true;
        });

        services.AddOptions<TestSettings>().Bind(Configuration.GetSection("TestSettings"));
        services.AddXRetrySupport();

        Services = services;
    }

    public void Configure(IConfiguration configuration,
        IOptions<TestSettings> testSettings
        )
    {

    }
}