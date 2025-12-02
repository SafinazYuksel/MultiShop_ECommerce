using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.WebUI.Services.Abstract;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IFileService _fileService;

        public BrandController(IBrandService brandService, IFileService fileService)
        {
            _brandService = brandService;
            _fileService = fileService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            BrandViewBagList();
            var values = await _brandService.GetAllBrandAsync();
            return View(values);
        }

        [Route("CreateBrand")]
        [HttpGet]
        public IActionResult CreateBrand()
        {
            BrandViewBagList();
            return View();
        }

        [Route("CreateBrand")]
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto, IFormFile formFile)
        {
            string imagePath = await _fileService.UploadFileAsync(formFile, "images/specialOfferCoverImages/");

            if (imagePath != null)
            {
                createBrandDto.ImageUrl = imagePath;
            }

            await _brandService.CreateBrandAsync(createBrandDto);
            return RedirectToAction("Index", "Brands", new { area = "Admin" });
        }

        [Route("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            await _brandService.DeleteBrandAsync(id);
            return RedirectToAction("Index", "Brands", new { area = "Admin" });
        }

        [Route("UpdateBrand/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            BrandViewBagList();
            var value = await _brandService.GetByIdBrandAsync(id);
            return View(value);
        }

        [Route("UpdateBrand/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto, IFormFile formFile)
        {
            string imagePath = await _fileService.UploadFileAsync(formFile, "images/specialOfferCoverImages/");

            if (imagePath != null)
            {
                updateBrandDto.ImageUrl = imagePath;
            }

            await _brandService.UpdateBrandAsync(updateBrandDto);
            return RedirectToAction("Index", "Brands", new { area = "Admin" });
        }

        void BrandViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Markalar";
            ViewBag.v3 = "Marka Listesi";
            ViewBag.v0 = "Marka İşlemleri";
        }
    }
}
