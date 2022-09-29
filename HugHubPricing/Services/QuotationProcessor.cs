using HugHubPricing.Models;
using HugHubPricing.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugHubPricing.Adapters;
using HugHubPricing.QuotationSystems;
using HugHubPricing.Interfaces;

namespace HugHubPricing.Processors
{    
    public class QuotationProcessor : IQuotationProcessor
    {
        private readonly IQuotationSystem QuotationSystem;
        private readonly IRequestAdapter RequestAdapter;
        private readonly IProcessingCriteria ProcessingCriteria;

        public QuotationProcessor(IQuotationSystem quotationSystem, IRequestAdapter requestAdapter, IProcessingCriteria processingCriteria)
        {
            this.QuotationSystem = quotationSystem ?? throw new ArgumentNullException(nameof(quotationSystem));
            this.RequestAdapter = requestAdapter ?? throw new ArgumentNullException(nameof(requestAdapter));
            this.ProcessingCriteria = processingCriteria ?? throw new ArgumentNullException(nameof(processingCriteria));
        }

        public PricingResult ProcessQuotation(PricingRequest request, PricingResult result)
        {
            if (this.ProcessingCriteria.IsPreprocessingCriteriaSatsfied(request))
            {
                dynamic adaptedRequest = this.RequestAdapter.Adapt(request);

                dynamic systemResponse = this.QuotationSystem.GetPrice(adaptedRequest);

                if (this.ProcessingCriteria.IsPostProcessingCriteriaSatsfied(systemResponse, result.Price))
                {
                    result.Price = systemResponse.Price;
                    result.InsurerName = systemResponse.Name;
                    result.Tax = systemResponse.Tax;
                }

            }

            return result;
        }
    }
}
