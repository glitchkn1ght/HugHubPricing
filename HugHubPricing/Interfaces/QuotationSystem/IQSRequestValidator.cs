using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IQSRequestValidator : IDependancy
    {
        public bool ValidateRequest(PricingRequest request);
    }
}
