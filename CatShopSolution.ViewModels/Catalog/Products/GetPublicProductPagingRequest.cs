using CatShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatShopSolution.ViewModels.Catalog.Products
{
    public class GetPublicProductPagingRequest : PadingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
