using System;
using System.Collections.Generic;
using System.Text;

namespace HugHubPricing.Models
{
    public class QuotationSystemDependancyOptions
    {
        public QuotationSystemDependancyOptions(    string systemName,
                                                    string requestValidatorId, 
                                                    string quotationServiceId, 
                                                    string responseValidatorId,
                                                    string responseMapperId)
        {
            this.SystemName = systemName;
            this.RequestValidatorId = requestValidatorId;
            this.QuotationServiceId = quotationServiceId;
            this.ResponseValidatorId = responseValidatorId;
            this.ResponseMapperId = responseMapperId;
        }


        public string SystemName { get; set; }

        public string RequestValidatorId { get; set; }
        
        public string QuotationServiceId { get; set; }

        public string ResponseValidatorId { get; set; }

        public string ResponseMapperId { get; set; }

    }

    public class QuotationServiceDepedancyOptions
    {
        public QuotationServiceDepedancyOptions(string serviceId, string serviceSettingsId, string externalServiceId, string requestMapperId)
        {
            this.QuotationServiceId = serviceId;
            this.QuotationServiceSettingsId = serviceSettingsId;
            this.ExternalServiceId = externalServiceId;
            this.RequestMapperId = requestMapperId;
        }
        public string QuotationServiceId { get; set; }   

        public string QuotationServiceSettingsId { get; set; }
        
        public string ExternalServiceId { get; set; }
        
        public string RequestMapperId { get; set; }
    }
}
