namespace HugHubPricing.Models.Results
{
    public class PricingResult
    {
        public PricingResult()
        {
            this.Error = new ErrorResult();
        }

        public decimal? Price { get; set; } = null;

        public decimal? Tax { get; set; } = null;

        public string InsurerName { get; set; }

        public ErrorResult Error { get; set; }
    }
}
