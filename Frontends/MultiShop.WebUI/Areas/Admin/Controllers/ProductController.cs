using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ProductViewBagList();
            var values = await _productService.GetAllProductAsync();
            return View(values);
        }

        [Route("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            ProductViewBagList();
            var values = await _productService.GetProductsWithCategoryAsync();
            return View(values);
        }

        [HttpGet]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();

            ViewBag.categories = categoryValues;
            return View();
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                var extension = Path.GetExtension(formFile.FileName);
                var imageName = Guid.NewGuid() + extension;

                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/productCoverImages/", imageName);

                using (var stream = new FileStream(location, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                createProductDto.ProductImageUrl = "/images/productCoverImages/" + imageName;
            }

            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var values1 = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categoryValues = (from x in values1
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();

            ViewBag.categories = categoryValues;

            var value = await _productService.GetByIdProductAsync(id);
            return View(value);
        }

        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                var extension = Path.GetExtension(formFile.FileName);
                var imageName = Guid.NewGuid() + extension;

                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/productCoverImages/", imageName);

                using (var stream = new FileStream(location, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                updateProductDto.ProductImageUrl = "/images/productCoverImages/" + imageName;
            }

            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        void ProductViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";
            ViewBag.v0 = "Ürün İşlemleri";
        }
    }
}
