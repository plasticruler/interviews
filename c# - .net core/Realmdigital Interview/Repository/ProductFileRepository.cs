using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Realmdigital_Interview.Entities;
using System.IO;
using System.Security.Permissions;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Realmdigital_Interview.Configuration;
using System.Threading.Tasks;

namespace Realmdigital_Interview.Repository
{
    public class ProductFileRepository : IProductRepository
    {
        string _fileLocation;
        List<ApiResponseProduct> _allProducts = null;
        IMapper _mapper;
        ILogger _logger;
        public ProductFileRepository(ILogger<ProductFileRepository> logger,IMapper mapper,  IConfigurationData configurationData)
        {
            _fileLocation = configurationData.FileLocation; //assume you may cache this file contents (injected as a Singleton)
            _logger = logger;
            _mapper = mapper;
            _logger.LogInformation("Repository is in constructor");
            if (File.Exists(_fileLocation))
            {
               _logger.LogInformation("Repository file found."); 
                string fileContents = File.ReadAllText(_fileLocation);
                _logger.LogDebug(fileContents);
                _allProducts = JsonConvert.DeserializeObject<List<ApiResponseProduct>>(fileContents);
                _logger.LogInformation("Repository contents read into memory.");
                              
            }
            else
               throw new FileNotFoundException(_fileLocation);
        }
        public async  Task<DtoApiResponseProduct> GetProductById(string Id)
        {
            var v = _allProducts.FirstOrDefault(x=>x.BarCode==Id);
            return await Task.Run(() => _mapper.Map<ApiResponseProduct, DtoApiResponseProduct>(v));
        }

        public async Task<List<DtoApiResponseProduct>> GetProductsByName(string productName)
        {
            var v = _allProducts.Where(x=>x.ItemName == productName);
            return await Task.Run(()=>_mapper.Map<List<ApiResponseProduct>,List<DtoApiResponseProduct>>(v.ToList()));
        }       
    }
}