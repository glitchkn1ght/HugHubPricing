using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HugHubPricing.Validation.QuotationSystem.Request
{
    public class QS1RequestValidator : IQSRequestValidator
    {
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
