﻿using HugHubPricing.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.QuotationSystems
{
    public class QuotationSystem3 : IQuotationSystem
    {
        public QuotationSystem3(string url, string port)
        {

        }

        public dynamic GetPrice(dynamic request)
        {
            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 92.67M;
            response.IsSuccess = true;
            response.Name = "zxcvbnm";
            response.Tax = 92.67M * 0.12M;

            return response;
        }
    }
}
