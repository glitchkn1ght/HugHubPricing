using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using HugHubPricing.Validation;
using System;
using System.Dynamic;

namespace HugHubPricing.QuotationSystems
{
    public class QuotationSystem1 : IQuotationSystem
    {
        private readonly IRequestMapper RequestMapper;
        private readonly IResponseMapper ResponseMapper;
        private readonly IResponseValidator ResponseValidator;

        public QuotationSystem1(string url, string port, IRequestMapper requestMapper, IResponseMapper responseMapper, IResponseValidator responseValidator)
        {
            this.RequestMapper = requestMapper ?? throw new ArgumentNullException(nameof(requestMapper));
            this.ResponseMapper = responseMapper ?? throw new ArgumentNullException(nameof(responseMapper));
            this.ResponseValidator = responseValidator ?? throw new ArgumentNullException(nameof(responseValidator));
        }

        public PricingResult ProcessQuotation(PricingRequest request, PricingResult result)
        {
            if (request.RiskData.DOB == null)
            {
                return result;
            }

            dynamic systemResponse = this.GetPriceResponse(request);

            if (this.ResponseValidator.ValidateResponse(systemResponse))
            {
                result = this.ResponseMapper.MapResponse(result, systemResponse);
            }

            return result;
        }

        private dynamic GetPriceResponse(PricingRequest request)
        {
            dynamic MappedResponse = this.RequestMapper.MapRequestFields(request);

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 123.45M;
            response.IsSuccess = true;
            response.Name = "Test Name";
            response.Tax = 123.45M * 0.12M;

            return response;
        }
    }
}
