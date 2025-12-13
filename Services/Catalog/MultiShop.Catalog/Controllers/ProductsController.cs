using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var values = await _productService.GetAllProductAsync();
            return Ok(values);
        }

        [HttpGet("GetTop8Product")]
        public async Task<IActionResult> GetTop8Product()
        {
            var values = await _productService.GetTop8ProductAsync();
            return Ok(values);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllProductSortByPrice([FromQuery] string? sort)
        {
            var values = await _productService.GetAllProductSortByPriceAsync(sort);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProduct(string id)
        {
            var value = await _productService.GetByIdProductAsync(id);
            return Ok(value);
        }

        [HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var values = await _productService.GetProductsWithCategoryAsync();
            return Ok(values);
        }

        [HttpGet("GetProductsWithByCategoryId")]
        public async Task<IActionResult> GetProductsWithByCategoryId(string id)
        {
            var values = await _productService.GetProductsWithByCategoryIdAsync(id);
            return Ok(values);
        }

        [HttpGet("GetProductsBySubCategoryAsync")]
        public async Task<IActionResult> GetProductsBySubCategoryAsync(string id)
        {
            var values = await _productService.GetProductsBySubCategoryAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            return Ok("Ürün ekleme işlemi başarılı.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return Ok("Ürün güncelleme işlemi başarılı.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Ürün silme işlemi başarılı.");
        }
    }
}
