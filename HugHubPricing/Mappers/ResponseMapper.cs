using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System.Dynamic;

namespace HugHubPricing.Mappers
{
    public class ResponseMapper : IResponseMapper
    {
        public PricingResult MapResponse(PricingResult result, dynamic systemResponse)
        {
            result.Price = systemResponse.Price;
            result.InsurerName= systemResponse.Name;
            result.Tax = systemResponse.Tax;

            return result;
        }
    }
}
