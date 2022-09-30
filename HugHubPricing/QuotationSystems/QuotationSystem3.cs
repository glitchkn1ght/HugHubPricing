using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using HugHubPricing.Validation;
using System.Dynamic;

namespace HugHubPricing.QuotationSystems
{
    public class QuotationSystem3 : IQuotationSystem
    {
        private readonly IRequestMapper RequestMapper;
        private readonly IResponseMapper ResponseMapper;
        private readonly IResponseValidator ResponseValidator;

        public QuotationSystem3(string url, string port, IRequestMapper requestMapper, IResponseMapper responseMapper, IResponseValidator responseValidator)
        {
            this.RequestMapper = requestMapper; 
            this.ResponseMapper = responseMapper;
            this.ResponseValidator = responseValidator;
        }

        public PricingResult ProcessQuotation(PricingRequest request, PricingResult result)
        {
            dynamic systemResponse = this.GetPriceResponse(request);

            if (this.ResponseValidator.ValidateResponse(systemResponse) && systemResponse.Price < result.Price)
            {
                this.ResponseMapper.MapResponse(result,request);
            }

            return result;
        }

        public dynamic GetPriceResponse(PricingRequest request)
        {
            dynamic MappedResponse = this.RequestMapper.MapRequestFields(request);

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(request);

            dynamic response = new ExpandoObject();
            response.Price = 92.67M;
            response.IsSuccess = true;
            response.Name = "zxcvbnm";
            response.Tax = 92.67M * 0.12M;

            return response;
        }
    }
}
