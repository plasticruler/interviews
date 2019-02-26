using NUnit.Framework;
using Moq;
using System;
using Realmdigital_Interview.Configuration;
using Realmdigital_Interview.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Realmdigital_Interview.MapperProfiles;

namespace Realmdigital_Interview.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        Mock<IConfigurationData> _configuration;
        Mock<ILogger<ProductFileRepository>> _fileRepoLogger;
        Mock<ILogger<ProductVendorServiceRepository>> _vendorRepoLogger;
        ProductFileRepository _fileRepository;
        ProductVendorServiceRepository _vendorServiceRepository;
        [SetUp]
        public void SetUp(){
            _fileRepoLogger = new Mock<ILogger<ProductFileRepository>>();
            _vendorRepoLogger = new Mock<ILogger<ProductVendorServiceRepository>>();
            var mappingConfig  = new MapperConfiguration(mc=>{
                mc.AddProfile(new DomainProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _configuration = new Mock<IConfigurationData>();
            _configuration.Setup(x=>x.FileLocation).Returns("../../../Tests/test-data.json");
            _configuration.Setup(x=>x.ServiceUrl).Returns("http://google.com"); //just to demonstrate but practise is don't call web services etc from unit tests, leave to QA/App Support
            _fileRepository = new ProductFileRepository(_fileRepoLogger.Object,mapper, _configuration.Object);
            _vendorServiceRepository = new ProductVendorServiceRepository(_vendorRepoLogger.Object, mapper, _configuration.Object);      
        }
        [Test]
        public void FileRepository_RepositoryFile_ProductExists(){
            var product = _fileRepository.GetProductById("bar_code_1"); 
            Assert.IsNotNull(product);//found product            
            Assert.IsTrue(product.Result.Prices.Count>0); //prices loaded (automapper mapped prices from json file to dto object)
        }
         [Test]
        public void FileRepository_RepositoryFile_ProductDoesNotExists(){
            var product = _fileRepository.GetProductById("bar_code_1NOTEXIST");//don't find product
            Assert.IsNull(product.Result);
        }
    }
}