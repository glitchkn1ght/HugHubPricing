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
    public class RequestAdapterSystem3 : IRequestAdapter
    {
        public dynamic Adapt(PricingRequest request)
        {
            dynamic systemRequest3 = new ExpandoObject();
            systemRequest3.FirstName = request.RiskData.FirstName;
            systemRequest3.Surname = request.RiskData.LastName;
            systemRequest3.DOB = request.RiskData.DOB;
            systemRequest3.Make = request.RiskData.Make;
            systemRequest3.Amount = request.RiskData.Value;

            return systemRequest3;
        }
    }
}
