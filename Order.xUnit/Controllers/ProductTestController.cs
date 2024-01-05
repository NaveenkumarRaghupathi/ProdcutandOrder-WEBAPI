using AutoMapper;
using Moq;
using Order.BusinessLayer.Interfaces;
using Order.PresentationLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Order.xUnit.Controllers
{
    public class ProductTestController
    {
        private readonly IMock<IProductsService> _mockUserService;
        private readonly IMock<IMapper> _mockMapper;
        public ProductTestController() {
            _mockUserService = new Mock<IProductsService>();
            _mockMapper = new Mock<IMapper>();
        }
        [Fact]
        public void GetAllUsersSuccessTest()
        {
            var requestController = new ProductController(_mockUserService.Object, _mockMapper.Object);

            var response = requestController.GetAllProducts();
            Assert.NotNull(response);
        }

        [Fact]
        public void GetUserByIdSuccessTest()
        {
            var requestController = new ProductController(_mockUserService.Object, _mockMapper.Object);

            var response = requestController.GetProductById(1);
            Assert.NotNull(response);
        }
    }
}
