using HugHubPricing.Models;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IQuotationSystem
    {
        PricingResult ProcessQuotation(PricingRequest request, PricingResult result);
    }
}
