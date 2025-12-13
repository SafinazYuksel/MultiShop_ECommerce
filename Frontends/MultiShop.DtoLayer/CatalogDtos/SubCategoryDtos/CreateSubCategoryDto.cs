using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.CatalogDtos.SubCategoryDtos
{
    public class CreateSubCategoryDto
    {
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool SubCategoryStatus { get; set; }
        public string CategoryId { get; set; }
    }
}
