using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System.Dynamic;

namespace HugHubPricing.QuotationSystems
{
    public class QuotationSystem2 : IQuotationSystem
    {
        private readonly IRequestMapper RequestMapper;
        private readonly IResponseMapper ResponseMapper;
        private readonly IResponseValidator ResponseValidator;
        
        public QuotationSystem2(string url, string port, dynamic request, IRequestMapper requestMapper, IResponseMapper responseMapper, IResponseValidator responseValidator)
        {
            this.RequestMapper = requestMapper;
            this.ResponseMapper = responseMapper;
            this.ResponseValidator = responseValidator;
        }

        public PricingResult ProcessQuotation(PricingRequest request, PricingResult result)
        {
            if (request.RiskData.Make != "examplemake1" || request.RiskData.Make != "examplemake2" ||
                request.RiskData.Make != "examplemake3")
            {
                return result;
            }

            dynamic systemResponse = this.GetPriceResponse(request);

            if (this.ResponseValidator.ValidateResponse(systemResponse) && systemResponse.Price < result.Price)
            {
               result = this.ResponseMapper.MapResponse(result,request);
            }

            return result; 
        }
        private dynamic GetPriceResponse(dynamic request)
        {
            dynamic MappedResponse = this.RequestMapper.MapRequestFields(request);

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 234.56M;
            response.HasPrice = true;
            response.Name = "qewtrywrh";
            response.Tax = 234.56M * 0.12M;

            return response;
        }
    }
}

