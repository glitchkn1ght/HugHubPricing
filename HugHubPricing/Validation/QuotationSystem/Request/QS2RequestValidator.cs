using HugHubPricing.Interfaces;
using HugHubPricing.Models;

namespace HugHubPricing.Validation.QuotationSystem.Request
{
    public class QS2RequestValidator : IQSRequestValidator
    {
        public bool ValidateRequest(PricingRequest request)
        {
            if (request.RiskData.Make != "examplemake1" &&
                request.RiskData.Make != "examplemake2" &&
                request.RiskData.Make != "examplemake3")
            {
                return false;
            }

            return true;
        }
    }
}
