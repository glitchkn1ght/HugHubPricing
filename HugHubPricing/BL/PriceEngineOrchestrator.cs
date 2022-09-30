using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using HugHubPricing.QuotationSystems;
using HugHubPricing.Mappers;
using System;
using HugHubPricing.Validation;
using HugHubPricing.Validation.QuotationSystem.Request;
using HugHubPricing.Service;

namespace HugHubPricing.BL
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

            this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS1RequestValidator(), new QuotationPricingService1("http://quote-system-1.com", "1234", new StandardRequestMapper()), new StandardReponseValidator(), new StandardResponseMapper()));
            this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS2RequestValidator(), new QuotationPricingService2("http://quote-system-2.com", "1235", new QS2RequestMapper()), new QS2ResponseValidator(), new StandardResponseMapper()));
            this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS3RequestValidator(), new QuotationPricingService3("http://quote-system-3.com", "100", new StandardRequestMapper()), new StandardReponseValidator(), new StandardResponseMapper()));


            PricingResult result = this.PriceEngine.GetCheapestQuotationPrice(request);

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
