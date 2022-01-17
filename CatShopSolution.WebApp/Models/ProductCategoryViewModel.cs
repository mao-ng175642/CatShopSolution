using CatShopSolution.Application.System.Languages;
using CatShopSolution.ViewModels.Catalog.Categories;
using CatShopSolution.ViewModels.Catalog.ProductImage;
using CatShopSolution.ViewModels.Catalog.Products;
using CatShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatShopSolution.WebApp.Models
{
    public class ProductCategoryViewModel
    {
        public CategoryVm Category { get; set; }
        public PagedResult<ProductVm> Products { get; set; }
        public List<ProductVm> RelatedProducts { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }
    }
}
