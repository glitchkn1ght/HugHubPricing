using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Validation
{
    public class ProcessingCriteria_System1 : IProcessingCriteria
    {
        public bool IsPreprocessingCriteriaSatsfied(PricingRequest request)
        {
            if (request.RiskData.DOB != null)
            {
                return true;
            }

            return false;
        }

        public bool IsPostProcessingCriteriaSatsfied(dynamic systemResponse, decimal existingLowestPrice)
        {
            if (systemResponse.IsSuccess = true)
            {
                return true;
            }

            return false;
        }
    }
}
