using CatShopSolution.ViewModels.Catalog.Categories;
using CatShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatShopSolution.Admin.Services
{
    public class CategoryApiClient :BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFatory,
            IHttpContextAccessor httContextAccessor,
            IConfiguration configuartion): base(httpClientFatory, httContextAccessor, configuartion)
        {

        }
        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            return await GetListAsync<CategoryVm>("/api/categories?languageId=" + languageId);
        }
    }
}
