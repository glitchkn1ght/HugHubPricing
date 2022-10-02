using HugHubPricing.Interfaces;
using HugHubPricing.Models;

namespace HugHubPricing.Validation.QuotationSystem.Request
{
    public class QS2RequestValidator : IQSRequestValidator
    {
        public bool ValidateRequest(PricingRequest request)
        {
            if(request.RiskData.Make == null)
            {
                return false;
            }
            
            string make = request.RiskData.Make.Trim().ToLower();
            
            if (make == "examplemake1" ||
                make == "examplemake2" ||
                make== "examplemake3")
            {
                return true;
            }

            return false;
        }
    }
}
