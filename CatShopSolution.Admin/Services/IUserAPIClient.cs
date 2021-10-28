using CatShopSolution.ViewModels.Common;
using CatShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatShopSolution.Admin.Services
{
    public interface IUserAPIClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<PagedResult<UserVm>> GetUserPagings(GetUserPagingRequest request);

        Task<bool> RegisterUser(RegisterRequest registerRequest);

    }
}
