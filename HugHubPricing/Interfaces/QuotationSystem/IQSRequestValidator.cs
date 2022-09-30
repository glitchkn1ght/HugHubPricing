using HugHubPricing.Models;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IQSRequestValidator
    {
        public bool ValidateRequest(PricingRequest request);
    }
}
