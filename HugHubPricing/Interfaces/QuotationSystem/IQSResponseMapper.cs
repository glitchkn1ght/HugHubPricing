using HugHubPricing.Models;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IQSResponseMapper
    {
        public PricingResult MapResponse(dynamic systemResponse, PricingResult result);
    }
}
