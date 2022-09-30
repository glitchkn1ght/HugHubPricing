using HugHubPricing.Interfaces;
using HugHubPricing.Models;

namespace HugHubPricing.Validation.QuotationSystem.Request
{
    public class QS3RequestValidator : IQSRequestValidator
    {
        public bool ValidateRequest(PricingRequest request)
        {
            return true;
        }
    }
}
