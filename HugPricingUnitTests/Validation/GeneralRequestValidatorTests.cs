using NUnit.Framework;
using HugHubPricing;
using HugHubPricing.Models;
using System;
using HugHubPricing.Validation;
using HugHubPricing.Models.Results;
using HugHubPricingUnitTests.CommonTestData;

namespace HugPricingUnitTests.ValidationTests.GeneralRequestValidatorTests
{
    public class GeneralRequestValidatorTests
    {
        CommonTestingData TestData;
        GeneralRequestValidator RequestValidator;

        [SetUp]
        public void Setup()
        {
            this.TestData = new CommonTestingData();

            this.RequestValidator = new GeneralRequestValidator();
        }

        [Test]
        public void WhenRiskDataValid_ThenNoError()
        {
            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.TestData.Request, new PricingResult());

            Assert.AreEqual(0, result.Error.Code);
        }

        [Test]
        public void WhenRiskDataNull_ThenReturn110()
        {
            this.TestData.Request.RiskData = null;
            
            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.TestData.Request, new PricingResult());

            Assert.AreEqual(-110, result.Error.Code);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase(null)]
        public void WhenFirstNameNullOrEmptry_ThenReturn111(string testString)
        {
            this.TestData.Request.RiskData.FirstName = testString;

            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.TestData.Request, new PricingResult());

            Assert.AreEqual(-111, result.Error.Code);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase(null)]
        public void WhenLastNameNullOrEmptry_ThenReturn111(string testString)
        {
            this.TestData.Request.RiskData.LastName = testString;

            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.TestData.Request, new PricingResult());

            Assert.AreEqual(-112, result.Error.Code);
        }

        [Test]
        public void WhenValueIsZero_ThenReturn113()
        {
            this.TestData.Request.RiskData.Value = 0;

            PricingResult result = this.RequestValidator.ValidateGeneralPricingRequest(this.TestData.Request, new PricingResult());

            Assert.AreEqual(-113, result.Error.Code);
        }
    }
}