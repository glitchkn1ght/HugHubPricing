using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System.Dynamic;

namespace HugHubPricing.Service
{
    public class QuotationPricingService2: IQuotationPricingService
    {
        private readonly IQSRequestMapper RequestMapper;
        
        public QuotationPricingService2(string url, string port, IQSRequestMapper requestMapper)
        {
            this.RequestMapper = requestMapper;
        }

        public dynamic GetServiceResponse(PricingRequest request)
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
