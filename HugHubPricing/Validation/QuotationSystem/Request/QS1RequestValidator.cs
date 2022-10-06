using HugHubPricing.Interfaces;
using HugHubPricing.Models;

namespace HugHubPricing.Validation.QuotationSystem.Request
{
    public class QS1RequestValidator : IQSRequestValidator
    {
        public string Identifier { get; set; } = "QS1";

        public bool ValidateRequest(PricingRequest request)
        {
            if (request.RiskData.DOB == null)
            {
                return false;
            }

            return true;
        }
    }
}
