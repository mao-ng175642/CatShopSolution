
using CatShopSolution.ViewModels.Catalog.Products;
using CatShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatShopSolution.Application.Products.Dtos
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId,GetPublicProductPagingRequest request);

    }
}
