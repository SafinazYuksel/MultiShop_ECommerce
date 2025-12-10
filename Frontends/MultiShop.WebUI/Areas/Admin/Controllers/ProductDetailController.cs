using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductDetail")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [Route("AddOrUpdateProductDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateProductDetail(string id)
        {
            ProductDetailViewBagList();

            var value = await _productDetailService.GetByIdProductDetailAsync(id);

            if (value == null)
            {
                ViewBag.IsUpdate = false;
                var newDetail = new UpdateProductDetailDto
                {
                    ProductId = id,
                    ProductDetailId = ""
                };
                return View(newDetail);
            }

            ViewBag.IsUpdate = true;
            return View(value);
        }

        [Route("AddOrUpdateProductDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            if (string.IsNullOrEmpty(updateProductDetailDto.ProductDetailId))
            {
                var createDto = new CreateProductDetailDto
                {
                    ProductId = updateProductDetailDto.ProductId,
                    ProductDescription = updateProductDetailDto.ProductDescription,
                    ProductInformation = updateProductDetailDto.ProductInformation
                };

                await _productDetailService.CreateProductDetailAsync(createDto);
            }
            else
            {
                await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            }

            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        void ProductDetailViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürün Detayları";
            ViewBag.v3 = "Ürün Detay Listesi";
            ViewBag.v0 = "Ürün Detay İşlemleri";
        }
    }
}
