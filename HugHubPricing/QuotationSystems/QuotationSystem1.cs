using HugHubPricing.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.QuotationSystems
{
    public class QuotationSystem1 : IQuotationSystem
    {
        public QuotationSystem1(string url, string port)
        {

        }

        public dynamic GetPrice(dynamic request)
        {
            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 123.45M;
            response.IsSuccess = true;
            response.Name = "Test Name";
            response.Tax = 123.45M * 0.12M;

            return response;
        }
    }
}
