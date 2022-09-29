using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Interfaces
{
    public interface IRequestValidator
    {
        PricingResult ValidateGeneralPricingRequest(PricingRequest request, PricingResult result);
    }
}
