
using CatShopSolution.Data.EF;
using CatShopSolution.Data.Entity;
using CatShopSolution.Utilitils.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CatShopSolution.ViewModels.Catalog.Products;
using CatShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using CatShopSolution.Application.Common;
using CatShopSolution.ViewModels.Catalog.ProductImage;

namespace CatShopSolution.Application.Catalog.Products.Dtos
{
    public class ManageProductService : IManageProductService
    {
        private readonly CatShopDbContext _dbContext;
        private readonly IStorageService _storageService;
        public ManageProductService(CatShopDbContext dbContext, IStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task AddViewcount(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };

            //Save image
            if (request.ThumbNailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbNailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbNailImage),
                        SortOrder = 1
                    }
                };
            }
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int> Delete(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) throw new CatShopException($"Can not find that product with id: {productId}");
            var images = _dbContext.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _dbContext.Products.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPadingRequest request)
        {
            //1. Select Join
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id
                        where pt.Name.Contains(request.Keyword)
                        select new { p, pt, pic };
            //2. Fillter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            if (request.CategoryId.Count > 0)
            {
                query = query.Where(p => request.CategoryId.Contains(p.pic.CategoryId));
            }
            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Description,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,

                }).ToListAsync();
            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Item = data
            };
            return pagedResult;

        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _dbContext.Products.FindAsync(request.Id);
            var productTranslations = await _dbContext.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id
             && x.LanguageId == request.LanguageId);
            if (product == null || productTranslations == null) throw new CatShopException($"Cannot find that product with id:{request.Id}");


            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;

            //Save image
            if (request.ThumbNailImage != null)
            {
                var thumbnailImage = await _dbContext.ProductImages.FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == request.Id);

                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbNailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbNailImage);
                    _dbContext.ProductImages.Update(thumbnailImage);
                }

            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) throw new CatShopException($"Cannot find that product with id: {productId}");
            product.Price = newPrice;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) throw new CatShopException($"Cannot find that product with id: {productId}");
            product.Stock += addedQuantity;
            return await _dbContext.SaveChangesAsync() > 0;
        }
/// <summary>
/// Get product  by id with languageId
/// </summary>
/// <param name="productId"></param>
/// <param name="languageId"></param>
/// <returns></returns>
        public async Task<ProductViewModel> GetById(int productId,string languageId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            var productTranslation = await _dbContext.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock =product.Stock,
                ViewCount = product.ViewCount,
            };
            return productViewModel;
        }
        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;         
            }
            _dbContext.ProductImages.Add(productImage);
             await _dbContext.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> RemoveImage( int imageId)
        {
            var productImage = await _dbContext.ProductImages.FindAsync(imageId);
            if(productImage == null)
            {
                throw new CatShopException($"Cannot find image with id: {imageId}");
            }
            _dbContext.ProductImages.Remove(productImage);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateImage( int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _dbContext.ProductImages.FindAsync(imageId);
            if(productImage == null)
            {
                throw new CatShopException($"Cannot find an image with id: {imageId}");
            }
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            };
            _dbContext.ProductImages.Update(productImage);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            return await _dbContext.ProductImages.Where(x => x.ProductId == productId)
                .Select(x => new ProductImageViewModel()
                {
                    Caption = x.Caption,
                    DateCreated = x.DateCreated,
                    FileSize = x.FileSize,
                    Id = x.Id,
                    IsDefault = x.IsDefault,
                    SortOrder = x.SortOrder,
                    ProductId = x.ProductId
                }).ToListAsync();
        }

        public  async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _dbContext.ProductImages.FindAsync(imageId);
            if(image == null)
            {
                throw new CatShopException($"Cannot find image");
            }
            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                IsDefault = image.IsDefault,
                SortOrder = image.SortOrder,
                ProductId = image.ProductId
            };
            return viewModel;

        }
    }
}
