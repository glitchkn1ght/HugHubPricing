using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Models.Results
{
    public class ErrorResult
    {
        public ErrorResult()
        {
            this.Code = 0;
            this.Message = string.Empty;  
        }

        public int Code { get; set; }  
        public string Message { get; set; }
    }
}
