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
using HugHubPricing.BusinessLogic;
using Microsoft.Extensions.Logging;
using HugHubPricing.Validation.QuotationSystem.Request;
using HugHubPricing.Service;
using HugHubPricing.Mappers;

namespace HugHub.IntegrationTests
{
    public class PrinceEngineIntegrationTests
    {
        private CommonTestingData testData;
        private Mock<ILogger<PriceEngine>> LoggerMock;
        private GeneralRequestValidator GeneralRequestValidator;
        PriceEngine PriceEngine;

        [SetUp]
        public void Setup()
        {
            this.testData = new CommonTestingData();
            this.LoggerMock = new Mock<ILogger<PriceEngine>>(); 
            this.GeneralRequestValidator = new GeneralRequestValidator();

            this.PriceEngine = new PriceEngine(this.LoggerMock.Object, this.GeneralRequestValidator);
        }

        [Test]
        public void WhenMultipleQuotationsRetrieved_ThenReturnLowestPrice()
        {
            this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS1RequestValidator(), new QuotationPricingService1("http://quote-system-1.com", "1234", new StandardRequestMapper()), new StandardReponseValidator(), new StandardResponseMapper()));
            this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS2RequestValidator(), new QuotationPricingService2("http://quote-system-2.com", "1235", new QS2RequestMapper()), new QS2ResponseValidator(), new StandardResponseMapper()));
            this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS3RequestValidator(), new QuotationPricingService3("http://quote-system-3.com", "100", new StandardRequestMapper()), new StandardReponseValidator(), new StandardResponseMapper()));

            PricingResult actualResult = this.PriceEngine.GetCheapestQuotationPrice(this.testData.Request);

            Assert.AreEqual(92.67M,actualResult.Price);
            Assert.AreEqual("zxcvbnm", actualResult.InsurerName);

        }
    }
}
