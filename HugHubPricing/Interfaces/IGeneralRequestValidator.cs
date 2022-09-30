using HugHubPricing.Models;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IGeneralRequestValidator
    {
        PricingResult ValidateGeneralPricingRequest(PricingRequest request, PricingResult result);
    }
}
