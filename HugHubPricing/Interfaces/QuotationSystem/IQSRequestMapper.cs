using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IQSRequestMapper
    {
        dynamic MapRequestFields(PricingRequest request);
    }
}
