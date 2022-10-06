using HugHubPricing.Interfaces;
using System.Dynamic;

namespace HugHubPricing.Service.ExternalServices
{
    public class ExternalService3 : IExternalService
    {
        public string Identifier { get; set; } = "ExternalService3";

        public dynamic CallService(dynamic request)
        {
            dynamic response = new ExpandoObject();
            response.Price = 92.67M;
            response.IsSuccess = true;
            response.Name = "zxcvbnm";
            response.Tax = 92.67M * 0.12M;

            return response;
        }
    }
}
