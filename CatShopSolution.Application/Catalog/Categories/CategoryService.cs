﻿using CatShopSolution.Data.EF;
using CatShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CatShopSolution.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly CatShopDbContext _dbContext;
        public CategoryService(CatShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            var query = from c in _dbContext.Categories
                        join ct in _dbContext.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name
            }).ToListAsync();

        }
    }
}