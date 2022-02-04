using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto responseDto;
        private IProductRepository productRepository;
        public ProductAPIController(IProductRepository repository)
        {
            productRepository=repository;
            responseDto=new ResponseDto(); 
        }
      
        //[Authorize]
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await productRepository.GetProducts();
                responseDto.Result = productDtos;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessages =
                    new List<string>() {
                        ex.ToString()
                    };
            }
            return responseDto;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDto = await productRepository.GetProductByID(id);
                responseDto.Result = productDto;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessages =
                    new List<string>() {
                        ex.ToString()
                    };
            }
            return responseDto;
        }

        [Authorize]
        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto product = await productRepository.CreateUpdateProduct(productDto);
                responseDto.Result = product;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessages =
                    new List<string>() {
                        ex.ToString()
                    };
            }
            return responseDto;
        }

        [Authorize]
        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto product = await productRepository.CreateUpdateProduct(productDto);
                responseDto.Result = product;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessages =
                    new List<string>() {
                        ex.ToString()
                    };
            }
            return responseDto;
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await productRepository.DeleteProduct(id);
                responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessages =
                    new List<string>() {
                        ex.ToString()
                    };
            }
            return responseDto;
        }
    }
}
