using HugHubPricing.Interfaces;
using HugHubPricing.Models;

namespace HugHubPricing.Validation
{
    public class ReponseValidator : IResponseValidator
    {
        public bool ValidateResponse(dynamic systemResponse)
        {
            if (systemResponse.IsSuccess)
            {
                return true;
            }

            return false;
        }
    }
}
