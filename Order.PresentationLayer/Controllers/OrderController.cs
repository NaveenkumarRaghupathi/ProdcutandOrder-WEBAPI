using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.BusinessLayer.Interfaces;
using Order.BusinessLayer.Services;
using Order.Contracts;
using Order.Contracts.V1.Request;
using Order.Contracts.V1.Response;
using Order.DataAccessLayer.Entities;

namespace Order.PresentationLayer.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public OrderController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Order.GetAll)]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _ordersService.GetOrdersAsync());
        }

        [HttpPost(ApiRoutes.Order.AddAsync)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModelRequest orderModelRequest)
        {
            dynamic? response = null;
            var errorResponse = new ErrorResponse();

            try
            {
                if (orderModelRequest == null || string.IsNullOrEmpty(orderModelRequest.ProductCode))
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = "Create Order",
                        Message = "Check the request/Unable to handle the request",
                    };
                    errorResponse.Errors.Add(errorModel);
                    return BadRequest(errorResponse);
                }
                else
                {
                    var addOrder = _mapper.Map<Orders>(orderModelRequest);
                    response = await _ordersService.AddOrdersAsync(addOrder);
                    if (response.Status == "InsufficientQty" && response.OrderQty < 0) {
                        var errorModel = new ErrorModel
                        {
                            FieldName = "Create Order",
                            Message = "Insufficient stock for the product.",
                        };
                        errorResponse.Errors.Add(errorModel);
                        return BadRequest(errorResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorModel
                {
                    FieldName = "Create Order",
                    Message = ex.ToString(),
                };
                errorResponse.Errors.Add(errorModel);
                return BadRequest(errorResponse);
            }
            return Ok(response);
        }

        [HttpPost(ApiRoutes.Order.CancelOrderAsync)]
        public async Task<IActionResult> CancelOrder(int orderID, string orderCode)
        {
            dynamic? response = null;
            var errorResponse = new ErrorResponse();

            try
            {
                if (orderID == 0 || string.IsNullOrEmpty(orderCode))
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = "Cancel Order",
                        Message = "Check the request/Unable to handle the request",
                    };
                    errorResponse.Errors.Add(errorModel);
                    return BadRequest(errorResponse);
                }
                else
                {
                    response = await _ordersService.CancelOrdersAsync(orderID, orderCode);
                }
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorModel
                {
                    FieldName = "Cancel Order",
                    Message = ex.ToString(),
                };
                errorResponse.Errors.Add(errorModel);
                return BadRequest(errorResponse);
            }
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Order.GetById)]
        public async Task<IActionResult> GetOrderById(int id)
        {
            return Ok(await _ordersService.GetOrdersByIdAsync(id));
        }
    }
}
