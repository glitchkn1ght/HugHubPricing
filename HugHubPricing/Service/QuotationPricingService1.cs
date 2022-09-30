using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System.Dynamic;

namespace HugHubPricing.Service
{
    public class QuotationPricingService1 : IQuotationPricingService
    {
        private readonly IQSRequestMapper RequestMapper;
        
        public QuotationPricingService1(string url, string port, IQSRequestMapper requestMapper)
        {
            this.RequestMapper = requestMapper;
        }

        public dynamic GetServiceResponse(PricingRequest request)
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
