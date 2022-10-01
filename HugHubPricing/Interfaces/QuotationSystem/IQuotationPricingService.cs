using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IQuotationPricingService
    {
        public dynamic GetServiceResponse(PricingRequest request);
    }
}