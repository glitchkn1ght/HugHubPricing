using NUnit.Framework;
using HugHubPricing;
using HugHubPricing.Models;
using System;
using HugHubPricing.Validation;
using HugHubPricing.Models.Results;
using System.Dynamic;
using HugPricingUnitTests.CommonTestData;


namespace HugPricingUnitTests.Validation.QuotationSystemValidatorTests
{
    public class QuotationSystem2ValidatorTests
    {
        CommonTestingData TestData;
        dynamic systemResponse;
        QuotationValidator_System2 validator_System2;

        [SetUp]
        public void Setup()
        {
            this.TestData = new CommonTestingData();

            this.systemResponse = new ExpandoObject();

            this.validator_System2 = new QuotationValidator_System2();
        }

        [TestCase("examplemake1")]
        [TestCase("examplemake2")]
        [TestCase("examplemake3")]
        public void WhenMakeValid_ThenPreProcReturnsTrue(string testMake)
        {
            this.TestData.Request.RiskData.Make = testMake;

            bool actual = this.validator_System2.IsPreprocessingCriteriaSatsfied(this.TestData.Request);

            Assert.IsTrue(actual);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("someothermake")]
        public void WhenMakeInvalid_ThenPreProcReturnsFalse(string testMake)
        {
            this.TestData.Request.RiskData.Make = testMake;

            bool actual = this.validator_System2.IsPreprocessingCriteriaSatsfied(this.TestData.Request);

            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenHasPriceIsTrue_AndPriceLessThanExistingPrice_ThenPostProcReturnsTrue()
        {
            this.systemResponse.HasPrice = true;
            this.systemResponse.Price = 2.0M;

            bool actual = this.validator_System2.IsPostProcessingCriteriaSatsfied(this.systemResponse, 3.0M);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenHasPriceIsFalse_ThenPostProcReturnsFalse()
        {
            this.systemResponse.HasPrice = false;

            bool actual = this.validator_System2.IsPostProcessingCriteriaSatsfied(this.systemResponse, 1.0M);

            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenHasPriceIsTrue_AndPriceNotLessThanExistingPrice_ThenPostProcReturnsFalse()
        {
            this.systemResponse.HasPrice = true;
            this.systemResponse.Price = 2.0M;

            bool actual = this.validator_System2.IsPostProcessingCriteriaSatsfied(this.systemResponse, 1.0M);

            Assert.IsFalse(actual);
        }
    }
}
