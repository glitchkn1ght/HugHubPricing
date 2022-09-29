using HugHubPricing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Interfaces
{
    public interface IRequestAdapter
    {
        dynamic Adapt(PricingRequest request);
    }
}
