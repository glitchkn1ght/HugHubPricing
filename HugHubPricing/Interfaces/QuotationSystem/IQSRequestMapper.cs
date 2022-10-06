using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IQSRequestMapper : IDependancy
    {
        dynamic MapRequestFields(PricingRequest request);
    }
}
