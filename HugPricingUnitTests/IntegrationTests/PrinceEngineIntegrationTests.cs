using HugHubPricing.BusinessLogic;
using HugHubPricing.Mappers;
using HugHubPricing.Models.Results;
using HugHubPricing.QuotationSystems;
using HugHubPricing.Service;
using HugHubPricing.Validation;
using HugHubPricing.Validation.QuotationSystem.Request;
using HugHubPricingUnitTests.CommonTestData;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

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

        //[Test]
        //public void WhenMultipleQuotationsRetrieved_ThenReturnLowestPrice()
        //{
        //    this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS1RequestValidator(), new QuotationPricingService1("http://quote-system-1.com", "1234", new StandardRequestMapper()), new StandardReponseValidator(), new StandardResponseMapper()));
        //    this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS2RequestValidator(), new QuotationPricingService2("http://quote-system-2.com", "1235", new QS2RequestMapper()), new QS2ResponseValidator(), new StandardResponseMapper()));
        //    this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS3RequestValidator(), new QuotationPricingService3("http://quote-system-3.com", "100", new StandardRequestMapper()), new StandardReponseValidator(), new StandardResponseMapper()));

        //    this.testData.Request.RiskData.Make = "examplemake1";
        //    PricingResult actualResult = this.PriceEngine.GetCheapestQuotationPrice(this.testData.Request);

        //    Assert.AreEqual(92.67M,actualResult.Price);
        //    Assert.AreEqual("zxcvbnm", actualResult.InsurerName);
        //}

        //[Test]
        //public void WhenNoMatchingQuotation_ThenReturnNullPrice()
        //{
        //    this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS1RequestValidator(), new QuotationPricingService1("http://quote-system-1.com", "1234", new StandardRequestMapper()), new StandardReponseValidator(), new StandardResponseMapper()));
        //    this.PriceEngine.AddQuotationSystem(new QuotationSystem(new QS2RequestValidator(), new QuotationPricingService2("http://quote-system-2.com", "1235", new QS2RequestMapper()), new QS2ResponseValidator(), new StandardResponseMapper()));
          
        //    this.testData.RiskData.DOB = null;
              
        //    PricingResult actualResult = this.PriceEngine.GetCheapestQuotationPrice(this.testData.Request);

        //    Assert.AreEqual(null, actualResult.Price);
        //}
    }
}
