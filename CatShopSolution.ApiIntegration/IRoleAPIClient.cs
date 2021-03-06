using CatShopSolution.ViewModels.Common;
using CatShopSolution.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatShopSolution.ApiIntegration
{
    public interface IRoleAPIClient
    {
        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}
