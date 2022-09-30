using HugHubPricing.Models;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IQSRequestMapper
    {
        dynamic MapRequestFields(PricingRequest request);
    }
}
