using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Realmdigital_Interview.Configuration;
using Realmdigital_Interview.Entities;

namespace Realmdigital_Interview.Repository
{
    public class ProductVendorServiceRepository:IProductRepository
    {
        List<ApiResponseProduct> _allProducts = null;
        IMapper _mapper;
        ILogger _logger;
        IConfigurationData _configurationData;
        public ProductVendorServiceRepository(ILogger<ProductVendorServiceRepository> logger, IMapper mapper, IConfigurationData configurationData){
            _logger = logger;
            _mapper = mapper;
            _configurationData = configurationData;
        }
     public DtoApiResponseProduct GetProductById(string id)
        {
            string response = "";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json"; //you post this type but also tell service your supported media type (xml/json/whatever) a real REST service support content-negotiation
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                response = client.UploadString(_configurationData.ServiceUrl, "POST", "{ \"id\": \"" + id + "\" }"); //for this to be REST-compliant should be GET, pass in search param as querystring
            }
            return  _mapper.Map<ApiResponseProduct, DtoApiResponseProduct>(JsonConvert.DeserializeObject<ApiResponseProduct>(response));
        }

        public List<DtoApiResponseProduct> GetProductsByName(string productName)
        {
            string response = "";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                response = client.UploadString(_configurationData.ServiceUrl, "POST", "{ \"names\": \"" + productName + "\" }"); //for this to be REST-compliant should be GET, pass in search param as querystring
            }
            return  _mapper.Map<List<ApiResponseProduct>, List<DtoApiResponseProduct>>(JsonConvert.DeserializeObject<List<ApiResponseProduct>>(response));
        }   
    }
}