using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatShopSolution.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Name { get; set; }
       
        public string Description { get; set; }
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public IFormFile ThumbnailImage { get; set; }

    }
}
