using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IExternalService : IDependancy
    {
        dynamic CallService(dynamic request);
    }
}
