using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugHubPricing.Models;
using HugHubPricing.QuotationSystems;
using HugHubPricing.Models.Results;
using HugHubPricing.Interfaces;
using Microsoft.Extensions.Logging;

namespace HugHubPricing.BL
{
    public class PriceEngine : IPriceEngine
    {
        //pass request with risk data with details of a gadget, return the best price retrieved from 3 external quotation engines

        private readonly ILogger<PriceEngine> Logger;
        private readonly IRequestValidator RequestValidator;
        private List<IQuotationProcessor> QuotationProcessors;

        public PriceEngine(IRequestValidator requestValidator, ILogger<PriceEngine> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.RequestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
            this.QuotationProcessors = new List<IQuotationProcessor>();
        }
        
        public void addQuotationProcessor(IQuotationProcessor quotationProcessor)
        {
            this.QuotationProcessors.Add(quotationProcessor);
        }

        public PricingResult GetPrice(PricingRequest request)
        {
            PricingResult result = new PricingResult();

            try
            {
                throw new Exception("SomeException");

                result = this.RequestValidator.ValidateGeneralPricingRequest(request, result);

                if (result.Error.Code != 0)
                {
                    this.Logger.LogInformation($"Operation=GetPrice(PriceEngine), Status=Success, Message=Unable to Validate PricingRequest code:{result.Error.Code}, message:{result.Error.Message}");
                    return result;
                }

                this.Logger.LogInformation($"Operation=GetPrice(PriceEngine), Status=Success, Message=Request has been successfully validated.");
                
                //now call 3 external systems and get the best price
                foreach (IQuotationProcessor quotationProcessor in this.QuotationProcessors)
                {
                    result = quotationProcessor.ProcessQuotation(request, result);
                }

                if (result.Price == 0)
                {
                    result.Error.Code = -101;

                    result.Error.Message = "No quotes were found for request";
                    
                    this.Logger.LogInformation($"Operation=GetPrice(PriceEngine), Status=Success, Message=No quotes were found for this reqeust.");
                }

                return result;
            }

            catch (Exception ex)
            {
                result.Error.Code = -100;
                result.Error.Message = "Exception has occurred";

                this.Logger.LogError($"Operation=GetPrice(PriceEngine), Status=Failure, Message={ex.Message}");

                return result;
            }
        }
    }
}
