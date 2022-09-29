using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Interfaces
{
    public interface IQuotationProcessor
    {
        PricingResult ProcessQuotation(PricingRequest request, PricingResult result);
    }
}
