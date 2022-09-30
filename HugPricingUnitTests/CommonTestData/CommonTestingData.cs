using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System;

namespace HugHubPricingUnitTests.CommonTestData
{
    public class CommonTestingData
    {
        public PricingRequest Request;
        public RiskData RiskData;
        public PricingResult EmptyResult;
        public PricingResult CheapResult;
        public PricingResult MiddleResult;    
        public PricingResult ExpensiveResult;

        public CommonTestingData()
        {
            this.RiskData = new RiskData()
            {
                FirstName = "Luke",
                LastName = "Skywalker",
                Value = 1.0M,
                Make = "SomeMake",
                DOB = DateTime.Parse("01/10/2021")
            };

            this.Request = new PricingRequest()
            {
                RiskData = this.RiskData
            };

            this.EmptyResult = new PricingResult();

            this.CheapResult = new PricingResult()
            {
                Price = 50.45M,
                InsurerName = "Cheap Insurance Provider",
                Tax = 50.45M * 0.12M
            };

            this.MiddleResult = new PricingResult()
            {
                Price = 100.0M,
                InsurerName = "Middle Insuracne Provider",
                Tax = 100.0M * 0.12M
            };

            this.ExpensiveResult = new PricingResult()
            {
                Price = 250.60M,
                InsurerName = "zxcvbnm",
                Tax = 250.60M * 0.12M
            };
        }
    }
}
