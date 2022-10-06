using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IQuotationPricingService : IDependancy
    {
        public dynamic GetServiceResponse(PricingRequest request);
    }
}