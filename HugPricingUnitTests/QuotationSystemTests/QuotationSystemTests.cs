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

namespace HugHubPricingUnitTests.QuotationSystemTests
{
    public class QuotationSystemTests
    {
        CommonTestingData testData;
        private Mock<IQSRequestValidator> RequestValidatorMock;
        private Mock<IQuotationPricingService> QuotationPricingServiceMock;
        private Mock<IQSResponseMapper> ResponseMapperMock;
        private Mock<IQSResponseValidator> ResponseValidatorMock;
        private QuotationSystem quotationSystem;
        private dynamic systemResponse;

        [SetUp]
        public void Setup()
        {
            this.testData = new CommonTestingData();
            this.RequestValidatorMock = new Mock<IQSRequestValidator>();
            this.QuotationPricingServiceMock = new Mock<IQuotationPricingService>();
            this.ResponseMapperMock = new Mock<IQSResponseMapper>();
            this.ResponseValidatorMock = new Mock<IQSResponseValidator>();
            this.systemResponse = new ExpandoObject();
        }

        [Test]
        public void WhenConstructorCalledWithNullRequestValidator_ThenArgNullExceptionThrown()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("requestValidator"), delegate
                {
                    this.quotationSystem = new QuotationSystem
                    (
                       null,
                       this.QuotationPricingServiceMock.Object,
                       this.ResponseValidatorMock.Object,
                       this.ResponseMapperMock.Object
                    );
                });
        }

        [Test]
        public void WhenConstructorCalledWithNullService_ThenArgNullExceptionThrown()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("quotationPricingService"), delegate
                {
                    this.quotationSystem = new QuotationSystem
                    (
                       this.RequestValidatorMock.Object,
                       null,
                       this.ResponseValidatorMock.Object,
                       this.ResponseMapperMock.Object
                    );
                });
        }

        [Test]
        public void WhenConstructorCalledWithNullResponseValidator_ThenArgNullExceptionThrown()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("responseValidator"), delegate
                {
                    this.quotationSystem = new QuotationSystem
                    (
                       this.RequestValidatorMock.Object,
                       this.QuotationPricingServiceMock.Object,
                       null,
                       this.ResponseMapperMock.Object
                    );
                });
        }

        [Test]
        public void WhenConstructorCalledWithNullResponseMapper_ThenArgNullExceptionThrown()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("responseMapper"), delegate
                {
                    this.quotationSystem = new QuotationSystem
                    (
                       this.RequestValidatorMock.Object,
                       this.QuotationPricingServiceMock.Object,
                       this.ResponseValidatorMock.Object,
                       null
                    );
                });
        }

        [Test]
        public void WhenConstructorCalledWithValidArguements_ThenNoExceptionThrown()
        {
            Assert.DoesNotThrow(
                delegate
                {
                    this.quotationSystem = new QuotationSystem
                    (
                       this.RequestValidatorMock.Object,
                       this.QuotationPricingServiceMock.Object,
                       this.ResponseValidatorMock.Object,
                       this.ResponseMapperMock.Object
                    );
                });
        }

        [Test]
        public void WhenQuoteSuccessfullyRetrieved_ThenResultIsUpdated()
        {
            this.RequestValidatorMock.Setup(x => x.ValidateRequest(It.IsAny<PricingRequest>())).Returns(true);
            this.QuotationPricingServiceMock.Setup(x => x.GetServiceResponse(It.IsAny<PricingRequest>())).Returns(this.systemResponse);
            this.ResponseValidatorMock.Setup(x => x.ValidateResponse(It.IsAny<It.IsAnyType>())).Returns(true);
            this.ResponseMapperMock.Setup(x => x.MapResponse(It.IsAny<It.IsAnyType>(), It.IsAny<PricingResult>())).Returns(this.testData.CheapResult);

            this.quotationSystem = new QuotationSystem(this.RequestValidatorMock.Object, this.QuotationPricingServiceMock.Object, this.ResponseValidatorMock.Object, this.ResponseMapperMock.Object);

            this.systemResponse.Price = this.testData.CheapResult.Price;

            PricingResult actual = this.quotationSystem.ProcessQuotation(this.testData.Request, this.testData.EmptyResult);

            Assert.AreEqual(this.testData.CheapResult.Tax, actual.Tax);
            Assert.AreEqual(this.testData.CheapResult.Price, actual.Price);
        }

        [Test]
        public void WhenRequestValidationFails_ThenResultIsNotUpdated()
        {
            this.RequestValidatorMock.Setup(x => x.ValidateRequest(It.IsAny<PricingRequest>())).Returns(false);
            this.QuotationPricingServiceMock.Setup(x => x.GetServiceResponse(It.IsAny<PricingRequest>())).Returns(this.systemResponse);
            this.ResponseValidatorMock.Setup(x => x.ValidateResponse(It.IsAny<It.IsAnyType>())).Returns(true);
            this.ResponseMapperMock.Setup(x => x.MapResponse(It.IsAny<It.IsAnyType>(), It.IsAny<PricingResult>())).Returns(this.testData.CheapResult);

            this.quotationSystem = new QuotationSystem(this.RequestValidatorMock.Object, this.QuotationPricingServiceMock.Object, this.ResponseValidatorMock.Object, this.ResponseMapperMock.Object);

            this.systemResponse.Price = this.testData.CheapResult.Price;

            PricingResult actual = this.quotationSystem.ProcessQuotation(this.testData.Request, this.testData.EmptyResult);

            Assert.AreEqual(0, actual.Tax);
            Assert.AreEqual(0, actual.Price);
        }


        [Test]
        public void WhenResponseValidationFails_ResultIsNotUpdated()
        {
            this.RequestValidatorMock.Setup(x => x.ValidateRequest(It.IsAny<PricingRequest>())).Returns(true);
            this.QuotationPricingServiceMock.Setup(x => x.GetServiceResponse(It.IsAny<PricingRequest>())).Returns(this.systemResponse);
            this.ResponseValidatorMock.Setup(x => x.ValidateResponse(It.IsAny<It.IsAnyType>())).Returns(false);
            this.ResponseMapperMock.Setup(x => x.MapResponse(It.IsAny<It.IsAnyType>(), It.IsAny<PricingResult>())).Returns(this.testData.CheapResult);

            this.quotationSystem = new QuotationSystem(this.RequestValidatorMock.Object, this.QuotationPricingServiceMock.Object, this.ResponseValidatorMock.Object, this.ResponseMapperMock.Object);

            PricingResult actual = this.quotationSystem.ProcessQuotation(this.testData.Request, this.testData.EmptyResult);

            Assert.AreEqual(0, actual.Tax);
            Assert.AreEqual(0, actual.Price);
        }
    }
}
