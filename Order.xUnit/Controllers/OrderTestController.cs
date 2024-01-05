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
    public class OrderTestController
    {
        private readonly IMock<IOrdersService> _mockUserService;
        private readonly IMock<IMapper> _mockMapper;
        public OrderTestController() {
            _mockUserService = new Mock<IOrdersService>();
            _mockMapper = new Mock<IMapper>();
        }
        [Fact]
        public void GetAllUsersSuccessTest()
        {
            var requestController = new OrderController(_mockUserService.Object, _mockMapper.Object);

            var response = requestController.GetAllOrders();
            Assert.NotNull(response);
        }

        [Fact]
        public void GetUserByIdSuccessTest()
        {
            var requestController = new OrderController(_mockUserService.Object, _mockMapper.Object);

            var response = requestController.GetOrderById(1);
            Assert.NotNull(response);
        }
    }
}
