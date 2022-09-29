using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugHubPricing.Interfaces
{
    public interface IQuotationSystem
    {
        dynamic GetPrice();

        dynamic GetPrice(dynamic request);
    }
}
