using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class ProductDetailReviewViewComponent : ViewComponent
    {
        private readonly ICommentService _commentService;

        public ProductDetailReviewViewComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new List<ResultCommentDto>());
            }

            try
            {
                var values = await _commentService.GetCommentListByProductId(id);

                if (values == null)
                {
                    return View(new List<ResultCommentDto>());
                }

                return View(values);
            }
            catch (System.Exception)
            {
                return View(new List<ResultCommentDto>());
            }
        }
    }
}
