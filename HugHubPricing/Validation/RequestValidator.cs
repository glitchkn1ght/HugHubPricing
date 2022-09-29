using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Validation
{
    public class RequestValidator : IRequestValidator
    {
        public PricingResult ValidateGeneralPricingRequest(PricingRequest request, PricingResult result)
        {

            if (request.RiskData == null)
            {
                result.Error.Code = -110;
                result.Error.Message = "Risk Data is missing";
                return result; 
            }

            if (string.IsNullOrEmpty(request.RiskData.FirstName))
            {
                result.Error.Code = -111;
                result.Error.Message = "First name is required";
                return result;
            }

            if (string.IsNullOrEmpty(request.RiskData.LastName))
            {
                result.Error.Code = -112;
                result.Error.Message = "Surname is required";
                return result;
            }

            if (request.RiskData.Value == 0)
            {
                result.Error.Code = -113;
                result.Error.Message = "Value is required";
                return result;
            }

            result.Error.Code = 0;

            return result; 
        }
    }
}
