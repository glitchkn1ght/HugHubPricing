using NUnit.Framework;
using HugHubPricing;
using HugHubPricing.Models;
using System;
using HugHubPricing.Validation;
using HugHubPricing.Models.Results;

namespace HugPricingUnitTests.ValidationTests
{
    public class Tests
    {
        PricingRequest request;
        RiskData RiskData;
        GeneralRequestValidator RequestValidator;

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

            this.RequestValidator = new GeneralRequestValidator();
        }

        [Test]
        public void WhenRiskDataValid_ThenNoError()
        {
            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.request, new PricingResult());

            Assert.AreEqual(0, result.Error.Code);
        }

        [Test]
        public void WhenRiskDataNull_ThenReturn110()
        {
            this.request.RiskData = null;
            
            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.request, new PricingResult());

            Assert.AreEqual(-110, result.Error.Code);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase(null)]
        public void WhenFirstNameNullOrEmptry_ThenReturn111(string testString)
        {
            this.request.RiskData.FirstName = testString;

            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.request, new PricingResult());

            Assert.AreEqual(-111, result.Error.Code);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase(null)]
        public void WhenLastNameNullOrEmptry_ThenReturn111(string testString)
        {
            this.request.RiskData.LastName = testString;

            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.request, new PricingResult());

            Assert.AreEqual(-112, result.Error.Code);
        }

        [Test]
        public void WhenValueIsZero_ThenReturn113()
        {
            this.request.RiskData.Value = 0;

            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.request, new PricingResult());

            Assert.AreEqual(-113, result.Error.Code);
        }
    }
}