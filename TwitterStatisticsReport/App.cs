using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;
using TwitterStatisticsReport.Interfaces;
using TwitterStatisticsReport.Models;

namespace TwitterStatisticsReport;

public static class App
{
    public static async Task Main(string[] args)
    {
        var config = CreateConfiguration();

        // create Host and Run
        await Host.CreateDefaultBuilder()
            .ConfigureServices((ctx, services) =>
            {
                services.AddLogging();
                services.AddHttpClient<IStreamService, StreamService>();
                services.AddSingleton<ITwitterService, TwitterService>();
                services.AddSingleton<ILoggerService, LoggerService>();
                services.Configure<Settings>(settings =>
                {
                    settings.ApiKey = config["ConsumerKey"];
                    settings.ApiSecret = config["ConsumerSecret"];
                    settings.ApiToken = config["AccessToken"];
                    settings.ApiTokenSecret = config["AccessTokenSecret"];
                    settings.BearerToken = config["BearerToken"];
                    settings.ApiUrl = config["ApiUrl"];
                });
                ctx.Configuration = config;
            })
            .Build()
            .Services.GetRequiredService<ITwitterService>()
            .ProcessTweets(CancellationToken.None);

    }

    private static IConfiguration CreateConfiguration()
    {
        var config = new ConfigurationBuilder();
        var configured = Configure(config);
        return configured.Build();
    }

    private static IConfigurationBuilder Configure(IConfigurationBuilder config)
    {
        var env = new HostingEnvironment
        {
            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development",
            ApplicationName = AppDomain.CurrentDomain.FriendlyName,
            ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
            ContentRootFileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory)
        };
        return config
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.ApplicationName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}