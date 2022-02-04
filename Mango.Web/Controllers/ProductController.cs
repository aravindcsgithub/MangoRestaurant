using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IPrpductService productService;
        public ProductController(IPrpductService _productService)
        {
            productService = _productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> lstProducts = new List<ProductDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await productService.GetAllPrpductAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                lstProducts = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(lstProducts);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View(new ProductDto());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await productService.CreateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("ProductIndex");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> ProductEdit(int productID)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await productService.GetProductsByIDAsync<ResponseDto>(productID, accessToken);
            if (response != null && response.IsSuccess)
            {
                ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(productDto);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await productService.UpdateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("ProductIndex");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> ProductDelete(int productID)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await productService.GetProductsByIDAsync<ResponseDto>(productID, accessToken);
            if (response != null && response.IsSuccess)
            {
                ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(productDto);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await productService.DeleteProductAsync<ResponseDto>(model.ProductId, accessToken);
            if (response.IsSuccess)
            {
                return RedirectToAction("ProductIndex");
            }
            return BadRequest();
        }
    }
}
