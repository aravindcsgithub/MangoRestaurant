using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPrpductService prductServices;
        public HomeController(ILogger<HomeController> logger, IPrpductService _prpductService)
        {
            _logger = logger;
            prductServices = _prpductService;
        }

        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            List<ProductDto> productList = new List<ProductDto>();
            var response = await prductServices.GetAllPrpductAsync<ResponseDto>("");
            if(response!=null && response.IsSuccess)
            {
                productList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(productList);
        }
        [Authorize]
        public async Task<IActionResult> Details(int productID)
        {
            ProductDto productModel = new ProductDto();
            var response = await prductServices.GetProductsByIDAsync<ResponseDto>(productID,"");
            if (response != null && response.IsSuccess)
            {
                productModel = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            return View(productModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction(nameof(Index));
        }
        public IActionResult LogOut()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}