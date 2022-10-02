using HugHubPricing.Interfaces;

namespace HugHubPricing.Validation
{
    public class QS2ResponseValidator : IQSResponseValidator
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
