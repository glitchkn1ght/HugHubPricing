using NUnit.Framework;
using HugHubPricing;
using HugHubPricing.Models;
using System;
using HugHubPricing.Validation;
using HugHubPricing.Models.Results;
using System.Dynamic;

namespace HugPricingUnitTests.Validation.QuotationSystemValidatorTests
{
    public class QuotationSystem1ValidatorTests
    {
        PricingRequest request;
        RiskData RiskData;
        dynamic systemResponse;
        QuotationValidator_System1 validator_System1;

        [SetUp]
        public void Setup()
        {
            this.RiskData = new RiskData()
            {
                FirstName = "Luke",
                LastName = "Skywalker",
                Value = 1.0M,
                Make = "SomeMake",
                DOB = DateTime.Parse("01/10/2021")
            };

            this.request = new PricingRequest()
            {
                RiskData = this.RiskData
            };

            this.systemResponse = new ExpandoObject();

            this.validator_System1 = new QuotationValidator_System1();
        }

        [Test]  
        public void WhenDOBNull_ThenPreProcReturnsFalse()
        {
            this.RiskData.DOB = null;

            bool actual = this.validator_System1.IsPreprocessingCriteriaSatsfied(this.request);

            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenDOBNotNull_ThenPreProcReturnsTrue()
        {
            this.systemResponse.IsSuccess = true;

            bool actual = this.validator_System1.IsPreprocessingCriteriaSatsfied(this.request);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenResponseIsSuccessTrue_ThenPostProcReturnsTrue()
        {
            this.systemResponse.IsSuccess = true;

            bool actual = this.validator_System1.IsPostProcessingCriteriaSatsfied(this.systemResponse, 1);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenResponseIsSuccessFalse_ThenPostProcReturnsFalse()
        {
            this.systemResponse.IsSuccess = false;

            bool actual = this.validator_System1.IsPostProcessingCriteriaSatsfied(this.systemResponse, 1);

            Assert.IsFalse(actual);
        }

    }
}
