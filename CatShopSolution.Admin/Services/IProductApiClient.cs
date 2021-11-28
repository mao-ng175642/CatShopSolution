using CatShopSolution.ViewModels.Catalog.Products;
using CatShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatShopSolution.Admin.Services
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductVm>> GetPagings(GetProductPadingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        Task<ProductVm> GetById(int id, string languageId);


    }
}
