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

namespace HugHubPricing.BL
{
    public class PriceEngine : IPriceEngine
    {
        //pass request with risk data with details of a gadget, return the best price retrieved from 3 external quotation engines

        IRequestValidator RequestValidator;
        List<IQuotationProcessor> QuotationProcessors;

        public PriceEngine()
        {
        }

        public PriceEngine(IRequestValidator requestValidator)
        {
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
                result = this.RequestValidator.ValidateGeneralPricingRequest(request, result);

                if (result.Error.Code != 0)
                {
                    return result;
                }

                //now call 3 external systems and get the best price

                foreach (IQuotationProcessor quotationProcessor in this.QuotationProcessors)
                {
                    result = quotationProcessor.ProcessQuotation(request, result);
                }

                if (result.Price == 0)
                {
                    result.Error.Code = -101;
                    result.Error.Message = "No quotes were found for request";
                }

                return result;
            }

            catch (Exception ex)
            {
                result.Error.Code = -100;
                result.Error.Message = "Exception has occurred";

                return result;
            }
        }
    }
}
