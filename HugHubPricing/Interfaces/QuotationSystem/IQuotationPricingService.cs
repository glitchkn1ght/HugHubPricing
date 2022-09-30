using HugHubPricing.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HugHubPricing.Interfaces
{
    public interface IQuotationPricingService
    {
        public dynamic GetServiceResponse(PricingRequest request);
    }
}