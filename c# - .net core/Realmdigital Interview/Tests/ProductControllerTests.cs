using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Realmdigital_Interview.Configuration;
using Realmdigital_Interview.Controllers;
using Realmdigital_Interview.MapperProfiles;
using Realmdigital_Interview.Repository;

namespace Realmdigital_Interview.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        ProductController _productController;
        Mock<IConfigurationData> _configuration;
        Mock<ILogger<ProductFileRepository>> _fileRepoLogger;
        Mock<ILogger<ProductController>> _productControllerLogger;
        ProductFileRepository _fileRepository;

        [SetUp]
        public void SetUp(){
            _fileRepoLogger = new Mock<ILogger<ProductFileRepository>>();
            _productControllerLogger = new Mock<ILogger<ProductController>>();
            var mappingConfig  = new MapperConfiguration(mc=>{
                mc.AddProfile(new DomainProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _configuration = new Mock<IConfigurationData>();
            _configuration.Setup(x=>x.FileLocation).Returns("../../../Tests/test-data.json");
            _configuration.Setup(x=>x.ServiceUrl).Returns("http://google.com"); //just to demonstrate but practise is don't call web services etc from unit tests, leave to QA/App Support
            _fileRepository = new ProductFileRepository(_fileRepoLogger.Object,mapper, _configuration.Object);
            _productController = new ProductController(_fileRepository, _productControllerLogger.Object);
        }
        [Test]
        public void Test_ProductController_GetResponse(){
            var result = _productController.GetProductById("bar_code_1");
            Assert.IsNotNull(result);
        }
        [Test]
        public void Test_ProductController_IsCorrectResponse(){
            var result = _productController.GetProductById("bar_code_1");
            Assert.AreEqual(result.Result.Value.Errors.Count(),0);
            Assert.AreEqual(result.Result.Value.Content.Name,"item_name_1");
        }
    }
}