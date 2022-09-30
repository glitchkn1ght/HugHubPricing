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
