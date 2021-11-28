using System;
using System.Collections.Generic;
using System.Text;

namespace CatShopSolution.ViewModels.Common
{
    public class PadingRequestBase 
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
