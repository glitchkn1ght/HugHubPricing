using HugHubPricing.Interfaces;

namespace HugHubPricing.Validation
{
    public class QS2ResponseValidator : IQSResponseValidator
    {
        public string Identifier { get; set; } = "QS2";

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
