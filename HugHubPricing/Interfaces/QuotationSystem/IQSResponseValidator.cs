namespace HugHubPricing.Interfaces
{
    public interface IQSResponseValidator : IDependancy
    {
        bool ValidateResponse(dynamic systemResponse);
    }
}
