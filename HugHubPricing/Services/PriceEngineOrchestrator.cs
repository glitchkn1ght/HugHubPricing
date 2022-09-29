using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;

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
