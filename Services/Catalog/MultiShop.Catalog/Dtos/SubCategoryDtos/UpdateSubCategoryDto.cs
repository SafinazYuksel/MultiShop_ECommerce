namespace MultiShop.Catalog.Dtos.SubCategoryDtos
{
    public class UpdateSubCategoryDto
    {
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool SubCategoryStatus { get; set; }
        public string CategoryId { get; set; }
    }
}
