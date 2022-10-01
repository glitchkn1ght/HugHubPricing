using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IQSRequestValidator
    {
        public bool ValidateRequest(PricingRequest request);
    }
}
