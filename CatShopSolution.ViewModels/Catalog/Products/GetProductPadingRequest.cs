using CatShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatShopSolution.ViewModels.Catalog.Products
{
    public class GetProductPadingRequest : PadingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> CategoryIds { get; set; }

        public string LanguageId { get; set; }
    }
}
