using HugHubPricing.Config;
using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using Microsoft.Extensions.Options;
using System.Dynamic;

namespace HugHubPricing.Service
{
    public class QuotationPricingService : IQuotationPricingService
    {
        public string Identifier { get; set; }
        private readonly QuotationServiceSettings ServiceSettings;
        private readonly IQSRequestMapper RequestMapper;
        private IExternalService ExternalServiceCaller;

        public QuotationPricingService( 
                                        string identifier,
                                        QuotationServiceSettings serviceSettings, 
                                        IQSRequestMapper requestMapper,
                                        IExternalService serviceCaller)
        {
            this.Identifier = identifier;
            this.ServiceSettings = serviceSettings;
            this.RequestMapper = requestMapper;
            this.ExternalServiceCaller = serviceCaller;
        }

        public dynamic GetServiceResponse(PricingRequest request)
        {
            dynamic MappedResponse = this.RequestMapper.MapRequestFields(request);

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(MappedResponse);

            return this.ExternalServiceCaller.CallService(MappedResponse);
        }
    }
}
