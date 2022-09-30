using HugHubPricing.BL;
using HugHubPricing.Interfaces;
using HugHubPricing.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.IO;

namespace HugHubPricing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SNIP - collect input (risk data from the user)

            var services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            var serviceProvider = services.BuildServiceProvider();

            // entry to run app
             serviceProvider.GetService<PriceEngineOrchestrator>().RunPricingEngine();

        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // build config
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\")))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

            // configure logging
            services.AddLogging(builder => builder.AddSerilog(
                new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger()))
                .BuildServiceProvider();

            //Configure game and api settings
          //  services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
         //   services.Configure<GameSettings>(configuration.GetSection("GameSettings"));

            // add services:
            services.AddTransient<IGeneralRequestValidator, GeneralRequestValidator>();

            services.AddTransient<IPriceEngine, PriceEngine>();

            // add app
            services.AddTransient<PriceEngineOrchestrator>();
        }
    }
}
