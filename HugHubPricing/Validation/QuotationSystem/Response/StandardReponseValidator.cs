using HugHubPricing.Interfaces;

namespace HugHubPricing.Validation
{
    public class StandardReponseValidator : IQSResponseValidator
    {
        public string Identifier { get; set; } = "Standard";

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
