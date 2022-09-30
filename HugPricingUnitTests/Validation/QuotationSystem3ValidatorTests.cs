using NUnit.Framework;
using HugHubPricing;
using HugHubPricing.Models;
using System;
using HugHubPricing.Validation;
using HugHubPricing.Models.Results;
using System.Dynamic;

namespace HugPricingUnitTests.Validation.QuotationSystemValidatorTests
{
    public class QuotationSystem3ValidatorTests
    {
        PricingRequest request;
        RiskData RiskData;
        dynamic systemResponse;
        QuotationValidator_System3 validator_System3;

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

            this.validator_System3 = new QuotationValidator_System3();
        }

        [Test]
        public void WhenResponseIsSuccessTrue_AndPriceLowerThanExistingPrice_ThenPostProcReturnsTrue()
        {
            this.systemResponse.IsSuccess = true;
            this.systemResponse.Price = 1.0M;

            bool actual = this.validator_System3.IsPostProcessingCriteriaSatsfied(this.systemResponse, 2.0M);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenResponseIsSuccessTrue_AndPriceNotLowerThanExistingPrice_ThenPostProcReturnsFalse()
        {
            this.systemResponse.IsSuccess = true;
            this.systemResponse.Price =3.0M;

            bool actual = this.validator_System3.IsPostProcessingCriteriaSatsfied(this.systemResponse, 2.0M);

            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenResponseIsSuccessFalse_ThenPostProcReturnsFalse()
        {
            this.systemResponse.IsSuccess = false;

            bool actual = this.validator_System3.IsPostProcessingCriteriaSatsfied(this.systemResponse, 1);

            Assert.IsFalse(actual);
        }

    }
}
