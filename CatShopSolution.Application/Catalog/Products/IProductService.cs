
using CatShopSolution.ViewModels.Catalog.ProductImage;
using CatShopSolution.ViewModels.Catalog.Products;
using CatShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatShopSolution.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);
        Task<ProductVm> GetById(int productId,string languageId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task AddViewcount(int productId);

        Task<PagedResult<ProductVm>> GetAllPaging(GetProductPadingRequest request);

        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage( int imageId); 
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest productImage);
        Task<List<ProductImageViewModel>> GetListImage(int productId);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<PagedResult<ProductVm>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        Task<List<ProductVm>> GetFeaturedProducts(string languageId, int take);
        Task<List<ProductVm>> GetLatestProducts(string languageId, int take);
    }
}
