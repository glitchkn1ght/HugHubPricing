using HugHubPricing.BusinessLogic;
using HugHubPricing.Config;
using HugHubPricing.Interfaces;
using HugHubPricing.Mappers;
using HugHubPricing.Service;
using HugHubPricing.Service.ExternalServices;
using HugHubPricing.Validation;
using HugHubPricing.Validation.QuotationSystem.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

            // add services:

            //QuotationService settings
            services.Configure<QuotationServiceSettings>("QuotationService1", configuration.GetSection("QuotationServiceSettings:QuotationService1"));
            services.Configure<QuotationServiceSettings>("QuotationService2", configuration.GetSection("QuotationServiceSettings:QuotationService2"));
            services.Configure<QuotationServiceSettings>("QuotationService3", configuration.GetSection("QuotationServiceSettings:QuotationService3"));

            //Validators
            services.AddTransient<IGeneralRequestValidator, GeneralRequestValidator>();
            services.AddTransient<IQSRequestValidator, QS1RequestValidator>();
            services.AddTransient<IQSRequestValidator, QS1RequestValidator>();
            services.AddTransient<IQSRequestValidator, QS2RequestValidator>();
            services.AddTransient<IQSRequestValidator, QS3RequestValidator>();
            services.AddTransient<IQSResponseValidator, StandardReponseValidator>();
            services.AddTransient<IQSResponseValidator, QS2ResponseValidator>();

            //RequestMappers
            services.AddTransient<IQSRequestMapper, QS2RequestMapper>();
            services.AddTransient<IQSRequestMapper, StandardRequestMapper>();

            //Add external service
            services.AddTransient<IExternalService, ExternalService1>();
            services.AddTransient<IExternalService, ExternalService2>();
            services.AddTransient<IExternalService, ExternalService3>();

            //ResponseMappers
            services.AddTransient<IQSResponseMapper, StandardResponseMapper>();

            //Engine
            services.AddTransient<IPriceEngine, PriceEngine>();

            // add app
            services.AddTransient<PriceEngineOrchestrator>();
        }
    }
}
