using HexaShop.Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HexaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

    }
}
