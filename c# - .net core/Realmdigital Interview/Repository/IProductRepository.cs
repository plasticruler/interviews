using System.Collections.Generic;
using Realmdigital_Interview.Entities;

namespace Realmdigital_Interview.Repository
{
    public interface IProductRepository
    {
        DtoApiResponseProduct GetProductById(string Id);
        List<DtoApiResponseProduct> GetProductsByName(string productName); 
    }
}