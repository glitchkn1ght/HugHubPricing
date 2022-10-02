using HugHubPricing.Interfaces;

namespace HugHubPricing.Validation
{
    public class StandardReponseValidator : IQSResponseValidator
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
