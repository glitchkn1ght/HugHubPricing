using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;
using HugHubPricing.Processors;
using HugHubPricing.QuotationSystems;
using HugHubPricing.Adapters;
using HugHubPricing.Validation;

namespace HugHubPricing.Services
{
    public class PriceEngineOrchestrator
    {
        IPriceEngine PriceEngine;

        public PriceEngineOrchestrator(IPriceEngine priceEngine)
        {
            this.PriceEngine = priceEngine ?? throw new ArgumentNullException(nameof(priceEngine));
        }

        public void RunPricingEngine()
        {
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

            this.PriceEngine.addQuotationProcessor(new QuotationProcessor(new RequestAdapterSystem1(), new QuotationSystem1("http://quote-system-1.com", "1234"), new QuotationValidator_System1()));
            this.PriceEngine.addQuotationProcessor(new QuotationProcessor(new RequestAdapterSystem2(), new QuotationSystem2("http://quote-system-2.com", "1235",request), new QuotationValidator_System2()));
            this.PriceEngine.addQuotationProcessor(new QuotationProcessor(new RequestAdapterSystem3(), new QuotationSystem3("http://quote-system-3.com", "100"), new QuotationValidator_System3()));

            PricingResult result = this.PriceEngine.GetPrice(request);

            if (result.Error.Code != 0)
            {
                Console.WriteLine(string.Format("There was an error - {0}, {1}", result.Error.Code, result.Error.Message));
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
