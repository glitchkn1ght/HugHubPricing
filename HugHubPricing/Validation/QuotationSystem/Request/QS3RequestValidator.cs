using HugHubPricing.Interfaces;
using HugHubPricing.Models;

namespace HugHubPricing.Validation.QuotationSystem.Request
{
    public class QS3RequestValidator : IQSRequestValidator
    {
        public string Identifier { get; set; } = "QS3";

        public bool ValidateRequest(PricingRequest request)
        {
            return true;
        }
    }
}
