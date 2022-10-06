using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IQSResponseMapper : IDependancy
    {
        public PricingResult MapResponse(dynamic systemResponse, PricingResult result);
    }
}
