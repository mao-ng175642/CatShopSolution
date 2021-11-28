using CatShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatShopSolution.ViewModels.System.Users
{
   public class GetUserPagingRequest:PadingRequestBase
    {
        public string Keyword { get; set; }
    }
}
