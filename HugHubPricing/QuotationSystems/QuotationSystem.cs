using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System;
using System.Dynamic;

namespace HugHubPricing.QuotationSystems
{
    public class QuotationSystem: IQuotationSystem
    {
        private readonly IQSRequestValidator RequestValidator;
        private readonly IQuotationPricingService QuotationPricingService;
        private readonly IQSResponseValidator ResponseValidator;
        private readonly IQSResponseMapper ResponseMapper;

   

        public QuotationSystem(   
                                    IQSRequestValidator requestValidator,
                                    IQuotationPricingService quotationPricingService,
                                    IQSResponseValidator responseValidator,
                                    IQSResponseMapper responseMapper
                                    )
        {
            this.RequestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
            this.QuotationPricingService = quotationPricingService ?? throw new ArgumentNullException(nameof(quotationPricingService));
            this.ResponseValidator = responseValidator ?? throw new ArgumentNullException(nameof(responseValidator));
            this.ResponseMapper = responseMapper ?? throw new ArgumentNullException(nameof(responseMapper)); 
        }

        public PricingResult ProcessQuotation(PricingRequest request, PricingResult result)
        {
            if (!this.RequestValidator.ValidateRequest(request))
            {
                return result;
            }

            dynamic systemResponse = this.QuotationPricingService.GetServiceResponse(request);

            if (this.ResponseValidator.ValidateResponse(systemResponse) && this.ValidatePrice(result.Price, systemResponse.Price))
            {
               result = this.ResponseMapper.MapResponse(systemResponse, result);
            }

            return result; 
        }

        private bool ValidatePrice(decimal? existingPrice, decimal systemPrice)
        {
            if(existingPrice == null)
            {
                return true;
            }

            if(systemPrice < existingPrice)
            {
                return true;
            }

            return false;
        }
    }
}

