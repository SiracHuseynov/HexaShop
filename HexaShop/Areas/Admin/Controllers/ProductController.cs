using HexaShop.Business.Exceptions;
using HexaShop.Business.Services.Abstracts;
using HexaShop.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 2)
        {
            if(pageIndex < 0 || pageSize < 0)
            {
                var paginatedProductss = await _productService.GetPaginatedProductAsync(pageIndex = 1, pageSize = 2);
                return View(paginatedProductss);
            }

            var paginatedProducts = await _productService.GetPaginatedProductAsync(pageIndex, pageSize);
            return View(paginatedProducts);
        }

        //public IActionResult Index()
        //{
        //    var products = _productService.GetAllProducts();
        //    return View(products);
        //}

        public IActionResult Create()
        {
            return View(); ;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _productService.AddAsyncProduct(product);
            }
            catch(ImageFileNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existProduct = _productService.GetProduct(x => x.Id == id);

            if (existProduct == null)
                return NotFound();

            return View(existProduct);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _productService.UpdateProduct(product.Id, product);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existProduct = _productService.GetProduct(x => x.Id == id);

            if (existProduct == null)
                return NotFound();

            return View(existProduct);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _productService.DeleteProduct(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");
        }



    }
}
