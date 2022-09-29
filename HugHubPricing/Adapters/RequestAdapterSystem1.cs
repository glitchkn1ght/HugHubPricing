using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Adapters
{    
    public class RequestAdapterSystem1 : IRequestAdapter
    {
        public dynamic Adapt(PricingRequest request)
        {
            dynamic systemRequest1 = new ExpandoObject();
            systemRequest1.FirstName = request.RiskData.FirstName;
            systemRequest1.Surname = request.RiskData.LastName;
            systemRequest1.DOB = request.RiskData.DOB;
            systemRequest1.Make = request.RiskData.Make;
            systemRequest1.Amount = request.RiskData.Value;

            return systemRequest1;
        }
    }
}
