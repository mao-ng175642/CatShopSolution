using CatShopSolution.ViewModels.Catalog.Categories;
using CatShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatShopSolution.Admin.Services
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVm>> GetAll(string languageId);
    }
}
