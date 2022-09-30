using HugHubPricing.Interfaces;
using HugHubPricing.Models;

namespace HugHubPricing.Validation
{
    public class System2_ResponseValidator : IResponseValidator
    {
        public bool ValidateResponse(dynamic systemResponse)
        {
            if (systemResponse.HasPrice)
            {
                return true;
            }

            return true;
        }
    }
}
