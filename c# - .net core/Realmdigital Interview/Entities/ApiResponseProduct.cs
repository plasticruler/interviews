using System.Collections.Generic;

namespace Realmdigital_Interview.Entities
{
    public class ApiResponseProduct
    {
            public string BarCode { get; set; }
            public string ItemName { get; set; }
            public IList<ApiResponsePrice> PriceRecords { get; set; }
    }
}