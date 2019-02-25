using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Realmdigital_Interview.Entities;
using Realmdigital_Interview.Infrastructure;
using Realmdigital_Interview.Repository;

namespace Realmdigital_Interview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {        
        IProductRepository _repository;
        ILogger _logger;
        public ProductController(IProductRepository repository, ILogger<ProductController> logger){
            _repository = repository;
            _logger = logger;
            _logger.LogDebug("In ProductController constructor.");
        }
        [HttpGet]
        public ActionResult<ApiResponse<List<string>>> Get()
        {
            var result = new ApiResponse<List<string>>();
            result.SetContent(new List<string>{"welcome"});
            return result;
        }
        
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<DtoApiResponseProduct>> GetProductById(string id) //using this makes auto documenting the API easier and also declares return type
        {
            var r = new ApiResponse<DtoApiResponseProduct>();
            try{
                var result = _repository.GetProductById(id);                
                if (result!=null)
                {
                    r.SetContent(_repository.GetProductById(id));
                }
                    
            }
            catch(Exception x){
                r.AddError(new Error(){
                    Code = 1, //have a code lookup method based on exception
                    Message = x.Message //also store stacktrace if app run in special mode
                });
            }            
           return r;
        }

        [HttpGet("search/{productName}")]
        public ActionResult<ApiResponse<List<DtoApiResponseProduct>>> GetProductsByName(string productName)
        {
            var r = new ApiResponse<List<DtoApiResponseProduct>>();
            try{
                var result = _repository.GetProductsByName(productName);
                if (result!=null){                    
                    r.SetContent(result);
                }
            }
            catch(Exception x){
                r.AddError(new Error(){
                    Code = 1, //have a code lookup method based on exception
                    Message = x.Message
                });
            }            
           return r;
        }        
    }
}