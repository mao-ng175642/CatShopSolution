using CatShopSolution.Admin.Models;
using CatShopSolution.ApiIntegration;
using CatShopSolution.Utilitils.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CatShopSolution.Admin.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILanguageApiClient _languageApiCient;
        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            _languageApiCient = languageApiClient;
        }
        public async  Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageApiCient.GetAll();
            var navigationVm = new NavigationViewModel()
            {
                CurrentLanguageId = HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.DefaultLanguageId),
                Languages = languages.ResultObj
            };

            return View("Default",navigationVm);
        }
    }
}
