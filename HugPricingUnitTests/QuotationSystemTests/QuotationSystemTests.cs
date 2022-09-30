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
        public void WhenAllDataValidated_ResultIsUpdated()
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
        public void WhenRequestValidationFails_ResultIsNotUpdated()
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
