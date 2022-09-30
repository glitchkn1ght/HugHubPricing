using HugHubPricing.Models;
using System;

namespace HugPricingUnitTests.CommonTestData
{
    public class CommonTestingData
    {
        public PricingRequest Request;
        public RiskData RiskData;
        
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
        }
    }
}
