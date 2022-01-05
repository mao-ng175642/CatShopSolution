using CatShopSolution.Data.EF;
using CatShopSolution.ViewModels.Slides;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CatShopSolution.Application.System.SlideService
{
    public class SlideService : ISlideService
    {
        private readonly CatShopDbContext _context;

        public SlideService(CatShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<SlideVm>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x => x.SortOrder)
                 .Select(x => new SlideVm()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     Url = x.Url,
                     Image = x.Image
                 }).ToListAsync();

            return slides;
        }    
    }
}
