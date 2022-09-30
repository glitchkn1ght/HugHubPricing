using HugHubPricing.Interfaces;
using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace HugHubPricing.BL
{
    public class PriceEngine : IPriceEngine
    {
        //pass request with risk data with details of a gadget, return the best price retrieved from 3 external quotation engines

        private readonly ILogger<PriceEngine> Logger;
        private readonly IGeneralRequestValidator RequestValidator;
        private List<IQuotationSystem> QuotationSystems;

        public PriceEngine(IGeneralRequestValidator requestValidator, ILogger<PriceEngine> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.RequestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
            this.QuotationSystems = new List<IQuotationSystem>();
        }
        
        public void AddQuotationSystem(IQuotationSystem quotationSystem)
        {
            this.QuotationSystems.Add(quotationSystem);
        }

        public PricingResult GetPrice(PricingRequest request)
        {
            PricingResult result = new PricingResult();

            try
            {
                result = this.RequestValidator.ValidateGeneralPricingRequest(request, result);

                if (result.Error.Code != 0)
                {
                    this.Logger.LogInformation($"Operation=GetPrice(PriceEngine), Status=Success, Message=Unable to Validate PricingRequest code:{result.Error.Code}, message:{result.Error.Message}");
                    return result;
                }

                this.Logger.LogInformation($"Operation=GetPrice(PriceEngine), Status=Success, Message=Request has been successfully validated.");
                
                //now call 3 external systems and get the best price
                foreach (IQuotationSystem quotationSystem in this.QuotationSystems)
                {
                    result = quotationSystem.ProcessQuotation(request, result);
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
