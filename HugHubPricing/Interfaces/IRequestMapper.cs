using HugHubPricing.Models;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IRequestMapper
    {
        dynamic MapRequestFields(PricingRequest request);
    }
}
