using HugHubPricing.Interfaces;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Mappers
{
    public class StandardResponseMapper : IQSResponseMapper
    {
        public string Identifier { get; set; } = "Standard";
        public PricingResult MapResponse(dynamic systemResponse, PricingResult result)
        {
            result.Price = systemResponse.Price;
            result.InsurerName= systemResponse.Name;
            result.Tax = systemResponse.Tax;

            return result;
        }
    }
}
