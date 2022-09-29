using HugHubPricing.Models;
using HugHubPricing.Services;
using HugHubPricing.Validation;
using HugHubPricing.Adapters;
using HugHubPricing.BL;
using HugHubPricing.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using HugHubPricing.QuotationSystems;
using HugHubPricing.Processors;

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
            services.AddTransient<IRequestValidator, RequestValidator>();

            //services.AddTransient<IQuotationSystem, QuotationSystem1>();
            //services.AddTransient<IQuotationSystem, QuotationSystem2>();
            //services.AddTransient<IQuotationSystem, QuotationSystem3>();
            
            //services.AddTransient<IRequestAdapter, RequestAdapterSystem1>();
            //services.AddTransient<IRequestAdapter, RequestAdapterSystem2>();
            //services.AddTransient<IRequestAdapter, RequestAdapterSystem3>();


            //services.AddTransient<IQuotationProcessor, QuotationProcessor>(x => x.GetService();
            services.AddTransient<IPriceEngine, PriceEngine>();

            // add app
            services.AddTransient<PriceEngineOrchestrator>();
        }
    }
}
