using HugHubPricing.Interfaces;
using System.Dynamic;

namespace HugHubPricing.Service.ExternalServices
{
    public class ExternalService2 : IExternalService
    {
        public string Identifier { get; set; } = "ExternalService2";

        public dynamic CallService(dynamic request)
        {
            dynamic response = new ExpandoObject();
            response.Price = 234.56M;
            response.HasPrice = true;
            response.Name = "qewtrywrh";
            response.Tax = 234.56M * 0.12M;

            return response;
        }
    }
}
