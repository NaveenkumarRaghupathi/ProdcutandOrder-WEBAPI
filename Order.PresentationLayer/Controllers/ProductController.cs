using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Order.BusinessLayer.Interfaces;
using Order.Contracts;
using Order.Contracts.V1.Request;
using Order.Contracts.V1.Response;
using Order.DataAccessLayer.Entities;

namespace Order.PresentationLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;
        public ProductController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }


        [HttpGet(ApiRoutes.Product.GetAll)]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productsService.GetProductsAsync());
        }

        [HttpGet(ApiRoutes.Product.GetById)]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _productsService.GetProductsByIdAsync(id));
        }

        [HttpPost(ApiRoutes.Product.AddAsync)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModelRequest productModelRequest) {
            dynamic? response = null;
            var errorResponse = new ErrorResponse();

            try
            {
                if (productModelRequest == null || string.IsNullOrEmpty(productModelRequest.ProductName))
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = "Create Product",
                        Message = "Check the request/Unable to handle the request",
                    };
                    errorResponse.Errors.Add(errorModel);
                    return BadRequest(errorResponse);
                }
                else
                {
                    var addProduct = _mapper.Map<Products>(productModelRequest);
                    response = await _productsService.AddProductsAsync(addProduct);
                }
            }
            catch (Exception ex) {
                var errorModel = new ErrorModel
                {
                    FieldName = "Create Product",
                    Message = ex.ToString(),
                };
                errorResponse.Errors.Add(errorModel);
                return BadRequest(errorResponse);
            }
            return Ok(response);
        }

        [HttpPut(ApiRoutes.Product.UpdateAsync)]
        public async Task<IActionResult> UpdateProduct([FromBody] Products productModel)
        {
            dynamic? response = null;
            var errorResponse = new ErrorResponse();

            try
            {
                if (productModel.Id < 0)
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = "Update Product",
                        Message = "Check the request/Unable to handle the request",
                    };
                    errorResponse.Errors.Add(errorModel);
                    return BadRequest(errorResponse);
                }
                else
                {
                    response = await _productsService.UpdateProductsAsync(productModel);
                }
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorModel
                {
                    FieldName = "Update Product",
                    Message = ex.ToString(),
                };
                errorResponse.Errors.Add(errorModel);
                return BadRequest(errorResponse);
            }
            return Ok(response);
        }

        [HttpPost(ApiRoutes.Product.DeleteAsync)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            dynamic? response = null;
            var errorResponse = new ErrorResponse();

            try
            {
                if (id < 0)
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = "Update Product",
                        Message = "Check the request/Unable to handle the request",
                    };
                    errorResponse.Errors.Add(errorModel);
                    return BadRequest(errorResponse);
                }
                else
                {
                    response = await _productsService.DeleteProductsAsync(id);
                }
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorModel
                {
                    FieldName = "Update Product",
                    Message = ex.ToString(),
                };
                errorResponse.Errors.Add(errorModel);
                return BadRequest(errorResponse);
            }
            return Ok(response);
        }
    }
}
