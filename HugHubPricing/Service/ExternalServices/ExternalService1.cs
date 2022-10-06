using HugHubPricing.Interfaces;
using System.Dynamic;

namespace HugHubPricing.Service.ExternalServices
{
    public class ExternalService1 : IExternalService
    {
        public string Identifier { get; set; } = "ExternalServiceCaller1";

        public dynamic CallService(dynamic request)
        {
            dynamic response = new ExpandoObject();
            response.Price = 123.45M;
            response.IsSuccess = true;
            response.Name = "Test Name";
            response.Tax = 123.45M * 0.12M;

            return response;
        }
    }
}
