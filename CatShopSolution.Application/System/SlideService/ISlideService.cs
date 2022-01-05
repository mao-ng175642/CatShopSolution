using CatShopSolution.ViewModels.Slides;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatShopSolution.Application.System.SlideService
{
    public interface ISlideService
    {
        Task<List<SlideVm>> GetAll();
    }
}
