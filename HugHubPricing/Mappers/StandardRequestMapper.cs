using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using System.Dynamic;

namespace HugHubPricing.Mappers
{
    public class StandardRequestMapper : IQSRequestMapper
    {
        public dynamic MapRequestFields(PricingRequest request)
        {
            dynamic systemRequest = new ExpandoObject();
            systemRequest.FirstName = request.RiskData.FirstName;
            systemRequest.Surname = request.RiskData.LastName;
            systemRequest.DOB = request.RiskData.DOB;
            systemRequest.Make = request.RiskData.Make;
            systemRequest.Amount = request.RiskData.Value;

            return systemRequest;
        }
    }
}
