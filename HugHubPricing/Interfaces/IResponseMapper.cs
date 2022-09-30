using HugHubPricing.Models;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IResponseMapper
    {
        public PricingResult MapResponse(PricingResult result, dynamic systemResponse);
    }
}
