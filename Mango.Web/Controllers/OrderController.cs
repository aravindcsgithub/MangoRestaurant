using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IPrpductService productService;
        public OrderController(IPrpductService _productService)
        {
            productService = _productService;
        }
        public async Task<IActionResult> OrderIndex()
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
        
    }
}
