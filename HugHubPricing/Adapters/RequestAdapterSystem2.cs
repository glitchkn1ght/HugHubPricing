using HugHubPricing.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugHubPricing.Interfaces;

namespace HugHubPricing.Adapters
{    
    public class RequestAdapterSystem2 : IRequestAdapter
    {
        public dynamic Adapt(PricingRequest request)
        {
            dynamic systemRequest2 = new ExpandoObject();
            systemRequest2.FirstName = request.RiskData.FirstName;
            systemRequest2.LastName = request.RiskData.LastName;
            systemRequest2.Make = request.RiskData.Make;
            systemRequest2.Value = request.RiskData.Value;

            return systemRequest2;
        }
    }
}
