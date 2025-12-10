using Humanizer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.Abstract;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;
        private readonly IFileService _fileService;

        public ProductImageController(IProductImageService productImageService, IFileService fileService)
        {
            _productImageService = productImageService;
            _fileService = fileService;
        }

        [Route("AddOrUpdateProductImage/{id}")]
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateProductImage(string id)
        {
            ProductImageDetailViewBagList();

            var value = await _productImageService.GetByIdProductImageAsync(id);

            if (value == null)
            {
                ViewBag.IsUpdate = false;
                var newDetail = new UpdateProductImageDto
                {
                    ProductId = id,
                    ProductImageId = ""
                };
                return View(newDetail);
            }

            ViewBag.IsUpdate = true;
            return View(value);
        }

        [Route("AddOrUpdateProductImage/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateProductImage(UpdateProductImageDto updateProductImageDto, IFormFile formFile)
        {
            string imagePath = await _fileService.UploadFileAsync(formFile, "images/productDetailImages/");

            if (!string.IsNullOrEmpty(imagePath))
            {
                updateProductImageDto.ProductImage1 = imagePath;
            }

            if (!string.IsNullOrEmpty(imagePath))
            {
                updateProductImageDto.ProductImage2 = imagePath;
            }

            if (!string.IsNullOrEmpty(imagePath))
            {
                updateProductImageDto.ProductImage3 = imagePath;
            }

            if (!string.IsNullOrEmpty(imagePath))
            {
                updateProductImageDto.ProductImage4 = imagePath;
            }


            if (string.IsNullOrEmpty(updateProductImageDto.ProductImageId))
            {
                var createDto = new CreateProductImageDto
                {
                    ProductId = updateProductImageDto.ProductId,
                    ProductImage1 = updateProductImageDto.ProductImage1,
                    ProductImage2 = updateProductImageDto.ProductImage2,
                    ProductImage3 = updateProductImageDto.ProductImage3,
                    ProductImage4 = updateProductImageDto.ProductImage4
                };

                await _productImageService.CreateProductImageAsync(createDto);
            }
            else
            {
                await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            }

            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        [Route("ProductImageDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> ProductImageDetail(string id)
        {
            ProductImageDetailViewBagList();
            var value = await _productImageService.GetByIdProductImageAsync(id);
            return View(value);
        }

        [Route("ProductImageDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> ProductImageDetail(UpdateProductImageDto updateProductImageDto)
        {
            await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        void ProductImageDetailViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürün Resimleri";
            ViewBag.v3 = "Ürün Resim Listesi";
            ViewBag.v0 = "Ürün Resim İşlemleri";
        }
    }
}
