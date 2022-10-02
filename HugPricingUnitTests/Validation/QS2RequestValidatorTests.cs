using NUnit.Framework;
using HugHubPricing;
using HugHubPricing.Models;
using System;
using HugHubPricing.Validation;
using HugHubPricing.Models.Results;
using HugHubPricingUnitTests.CommonTestData;
using HugHubPricing.Validation.QuotationSystem.Request;

namespace HugPricingUnitTests.ValidationTests.GeneralRequestValidatorTests
{
    public class QS2RequestValidatorTests
    {
        CommonTestingData TestData;
        QS2RequestValidator QS2RequestValidator;

        [SetUp]
        public void Setup()
        {
            this.TestData = new CommonTestingData();

            this.QS2RequestValidator = new QS2RequestValidator();
        }

        [TestCase("examplemake1")]
        [TestCase("examplemake2")]
        [TestCase("examplemake3")]
        [TestCase("exampleMAKE1 ")]
        [TestCase(" EXAMPLEMAKE2 ")]
        [TestCase("  examplemake3  ")]
        public void WhenMakeValid_ThenValidatorReturnsTrue(string testMake)
        {
            this.TestData.Request.RiskData.Make = testMake;
            
            bool actual = this.QS2RequestValidator.ValidateRequest(this.TestData.Request);

            Assert.IsTrue(actual); 
        }

        [TestCase("someothermake1")]
        [TestCase("example make3")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("  ")]
        public void WhenMakeInvalid_ThenValidatorReturnsFalse(string testMake)
        {
            this.TestData.Request.RiskData.Make = testMake;

            bool actual = this.QS2RequestValidator.ValidateRequest(this.TestData.Request);

            Assert.IsFalse(actual);
        }
    }
}