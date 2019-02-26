using System.Collections.Generic;
using System.Threading.Tasks;
using Realmdigital_Interview.Entities;

namespace Realmdigital_Interview.Repository
{
    public interface IProductRepository
    {
        Task<DtoApiResponseProduct> GetProductById(string Id);
        Task<List<DtoApiResponseProduct>> GetProductsByName(string productName); 
    }
}