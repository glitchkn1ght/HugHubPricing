using HugHubPricing.Config;
using HugHubPricing.Interfaces;
using HugHubPricing.Mappers;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using HugHubPricing.QuotationSystems;
using HugHubPricing.Service;
using HugHubPricing.Validation;
using HugHubPricing.Validation.QuotationSystem.Request;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

using System.Collections.Generic;
using System.Linq;

namespace HugHubPricing.BusinessLogic
{
    public class PriceEngineOrchestrator
    {
        IPriceEngine PriceEngine;

        IEnumerable<IQSRequestMapper> RequestMappers;
        IEnumerable<IQSRequestValidator> RequestValidators;
        IOptionsSnapshot<QuotationServiceSettings> ServiceSettings;
        IEnumerable<IExternalService> ExternalServices;
        IEnumerable<IQSResponseValidator> ResponseValidators;
        IEnumerable<IQSResponseMapper> ResponseMappers;

        public PriceEngineOrchestrator(     IPriceEngine priceEngine,
                                            IEnumerable<IQSRequestMapper> requestMappers,
                                            IEnumerable<IQSRequestValidator> requestValidators,
                                            IOptionsSnapshot<QuotationServiceSettings> serviceSettings,
                                            IEnumerable<IExternalService> externalServices,
                                            IEnumerable<IQSResponseValidator> responseValidators,
                                            IEnumerable<IQSResponseMapper> responseMappers)
        {
            this.PriceEngine = priceEngine ?? throw new ArgumentNullException(nameof(priceEngine));
            this.RequestMappers = requestMappers;
            this.RequestValidators = requestValidators;
            this.ServiceSettings = serviceSettings; 
            this.ExternalServices = externalServices;
            this.ResponseValidators = responseValidators;
            this.ResponseMappers = responseMappers;
        }

        public List<IQuotationPricingService> MapQuotationServices()
        {
            //This list would come from database or configuration file. 
            IEnumerable<QuotationServiceDepedancyOptions> ServiceDepedancyOptions = new List<QuotationServiceDepedancyOptions>()
            {
                new QuotationServiceDepedancyOptions("QuotationService1","QuotationService1","ExternalService1","Standard"),
                new QuotationServiceDepedancyOptions("QuotationService2","QuotationService2", "ExternalService2", "QS2"),
                new QuotationServiceDepedancyOptions("QuotationService3","QuotationService3", "ExternalService3", "Standard")
            };

            List<IQuotationPricingService> quotationPricingServices = new List<IQuotationPricingService>();

            foreach (QuotationServiceDepedancyOptions option in ServiceDepedancyOptions)
            {
                quotationPricingServices.Add(
                  new QuotationPricingService
                      (
                        option.QuotationServiceId,
                        this.ServiceSettings.Get(option.QuotationServiceSettingsId),
                        this.RequestMappers.First(x => x.Identifier == option.RequestMapperId),
                        this.ExternalServices.First(x => x.Identifier == option.ExternalServiceId)
                      )
                  );
            }

            return quotationPricingServices;
        }


    public void MapQuoationSystems()
        {
            IEnumerable<IQuotationPricingService> quotationPricingServices = this.MapQuotationServices();

            //This list would come from database or configuration file. 
            IEnumerable<QuotationSystemDependancyOptions> depedancyOptionsAvail = new List<QuotationSystemDependancyOptions>()
            {
                new QuotationSystemDependancyOptions("QuotationSystem1","QS1","QuotationService1","Standard","Standard"),
                new QuotationSystemDependancyOptions("QuotationSystem1","QS2","QuotationService2","QS2","Standard"),
                new QuotationSystemDependancyOptions("QuotationSystem3","QS3","QuotationService3","Standard","Standard"),
            };
            
            foreach(QuotationSystemDependancyOptions option in depedancyOptionsAvail)
            {
                this.PriceEngine.AddQuotationSystem(
                  new QuotationSystem
                      (
                         this.RequestValidators.First(x => x.Identifier == option.RequestValidatorId),
                         quotationPricingServices.First(x => x.Identifier == option.QuotationServiceId),
                         this.ResponseValidators.First(x => x.Identifier == option.ResponseValidatorId),
                         this.ResponseMappers.First(x => x.Identifier == option.ResponseMapperId)
                      )
                  );
            }
        }

        public void RunPricingEngine()
        {
            this.MapQuoationSystems();

            var request = new PricingRequest()
            {
                RiskData = new RiskData() //hardcoded here, but would normally be from user input above
                {
                    DOB = DateTime.Parse("1980-01-01"),
                    FirstName = "John",
                    LastName = "Smith",
                    Make = "Cool New Phone",
                    Value = 500
                }
            };

            PricingResult result = this.PriceEngine.GetCheapestQuotationPrice(request);

            if (result.Error.Code != 0)
            {
                Console.WriteLine(string.Format("There was an error - {0}, {1}", result.Error.Code, result.Error.Message));
            }

            else if (result.Price == null)
            {
                Console.WriteLine(string.Format("There were no quotes found for this request"));
            }

            else
            {
                Console.WriteLine(string.Format("You price is {0}, from insurer: {1}. This includes tax of {2}", result.Price, result.InsurerName, result.Tax));
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
