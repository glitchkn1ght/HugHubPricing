using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System.Dynamic;

namespace HugHubPricing.Mappers
{
    public class QS2RequestMapper : IQSRequestMapper
    {
        public dynamic MapRequestFields(PricingRequest request)
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
