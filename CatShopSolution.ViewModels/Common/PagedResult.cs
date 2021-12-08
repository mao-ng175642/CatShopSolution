using System;
using System.Collections.Generic;
using System.Text;

namespace CatShopSolution.ViewModels.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Item { get; set; }
        //public int PageSize { get; set; }
        //public int PageIndex { get; set; }
    }
}
