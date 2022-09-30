using NUnit.Framework;
using HugHubPricing;
using HugHubPricing.Models;
using System;
using HugHubPricing.Validation;
using HugHubPricing.Models.Results;
using System.Dynamic;
using HugHubPricingUnitTests.CommonTestData;
using Moq;
using HugHubPricing.QuotationSystems;
using HugHubPricing.Interfaces;
using HugHubPricing.BL;
using Microsoft.Extensions.Logging;

namespace HugHubPricingUnitTests.BusinessLogicTests
{

    public class PriceEngineTests
    {
        private CommonTestingData testData;
        private Mock<IGeneralRequestValidator> GeneralRequestValidatorMock;
        private Mock<ILogger<PriceEngine>> LoggerMock;
        private Mock<IQuotationSystem> QuotationSystem1Mock;
        private Mock<IQuotationSystem> QuotationSystem2Mock;
        private PriceEngine PriceEngine;

        [SetUp]
        public void Setup()
        {
            this.testData = new CommonTestingData();
            this.GeneralRequestValidatorMock = new Mock<IGeneralRequestValidator>();
            this.QuotationSystem1Mock = new Mock<IQuotationSystem>();
            this.QuotationSystem2Mock = new Mock<IQuotationSystem>();
            this.LoggerMock = new Mock<ILogger<PriceEngine>>();
        }

        [TestCase(-110)]
        [TestCase(-111)]
        [TestCase(-112)]
        [TestCase(-113)]
        public void WhenGeneralValidationRequestFails_ThenReturnValidationErrorCode(int expectedCode)
        {
            PricingResult expected = new PricingResult();
            {
                ErrorResult error = new ErrorResult()
                {
                    Code = expectedCode
                };
            }

            this.GeneralRequestValidatorMock.Setup(x => x.ValidateGeneralPricingRequest(this.testData.Request, It.IsAny<PricingResult>())).Returns(expected);

            this.PriceEngine = new PriceEngine(this.LoggerMock.Object,this.GeneralRequestValidatorMock.Object);

            PricingResult actualResult = this.PriceEngine.GetCheapestQuotationPrice(this.testData.Request);

            Assert.AreEqual(expected.Error.Code, actualResult.Error.Code);  
            Assert.AreEqual(expected.Price, actualResult.Price);    
        }

        [Test]
        public void WhenQuoationsRetrieved_ThenReturnThatPrice()
        {
            PricingResult expected = new PricingResult();
            {
                ErrorResult error = new ErrorResult()
                {
                    Code = 0
                };
            }

            this.GeneralRequestValidatorMock.Setup(x => x.ValidateGeneralPricingRequest(this.testData.Request, It.IsAny<PricingResult>())).Returns(expected);

            this.QuotationSystem1Mock.Setup(x => x.ProcessQuotation(this.testData.Request, It.IsAny<PricingResult>())).Returns(this.testData.CheapResult);

            this.PriceEngine = new PriceEngine(this.LoggerMock.Object, this.GeneralRequestValidatorMock.Object);
            this.PriceEngine.AddQuotationSystem(this.QuotationSystem1Mock.Object);

            PricingResult actualResult = this.PriceEngine.GetCheapestQuotationPrice(this.testData.Request);

            Assert.AreEqual(this.testData.CheapResult.Price, actualResult.Price);
            Assert.AreEqual(this.testData.CheapResult.InsurerName, actualResult.InsurerName);
        }

    }
}
