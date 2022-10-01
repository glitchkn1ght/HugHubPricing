using HugHubPricing.Models;
using HugHubPricing.Models.Results;

namespace HugHubPricing.Interfaces
{
    public interface IPriceEngine
    {
        public PricingResult GetCheapestQuotationPrice(PricingRequest request);

        public void AddQuotationSystem(IQuotationSystem quotationSystem);
    }
}
