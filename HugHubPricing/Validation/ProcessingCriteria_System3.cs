using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Validation
{
    public class ProcessingCriteria_System3 : IProcessingCriteria
    {
        public bool IsPreprocessingCriteriaSatsfied(PricingRequest request)
        {
            if (request.RiskData.Make == "examplemake1" || request.RiskData.Make == "examplemake2" ||
                request.RiskData.Make == "examplemake3")
            {
                return true;
            }

            return false;
        }

        public bool IsPostProcessingCriteriaSatsfied(dynamic systemResponse, decimal existingLowestPrice)
        {
            if (systemResponse.IsSuccess && systemResponse.Price < existingLowestPrice)
            {
                return true;
            }

            return false;
        }
    }

}
