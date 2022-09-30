using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IResponseValidator
    {
        bool ValidateResponse(dynamic systemResponse);
    }
}
