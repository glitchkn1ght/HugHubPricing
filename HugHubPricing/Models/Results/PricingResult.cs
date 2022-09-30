﻿namespace HugHubPricing.Models.Results
{
    public class PricingResult
    {
        public PricingResult()
        {
            this.Error = new ErrorResult();
        }
        
        public decimal Price { get; set; }
        
        public decimal Tax { get; set; }

        public string InsurerName { get; set; }

        public ErrorResult Error { get; set; }

    }
}