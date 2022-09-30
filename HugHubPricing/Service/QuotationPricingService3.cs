using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System.Dynamic;

namespace HugHubPricing.Service
{
    public class QuotationPricingService3 : IQuotationPricingService
    {
        private readonly IQSRequestMapper RequestMapper;
        
        public QuotationPricingService3(string url, string port, IQSRequestMapper requestMapper)
        {
            this.RequestMapper = requestMapper;
        }

        public dynamic GetServiceResponse(PricingRequest request)
        {
            dynamic MappedResponse = this.RequestMapper.MapRequestFields(request);

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 92.67M;
            response.IsSuccess = true;
            response.Name = "zxcvbnm";
            response.Tax = 92.67M * 0.12M;

            return response;
        }
    }
}
