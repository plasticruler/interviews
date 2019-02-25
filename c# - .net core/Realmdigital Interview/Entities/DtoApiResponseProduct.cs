using System.Collections.Generic;

namespace Realmdigital_Interview.Entities
{
    public class DtoApiResponseProduct
    {
        public string Id{get;set;}
        public string Name {get;set;}
        public List<DtoApiResponsePrice> Prices {get;set;}
    }
}