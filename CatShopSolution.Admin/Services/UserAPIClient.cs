using CatShopSolution.ViewModels.Common;
using CatShopSolution.ViewModels.System.Users;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CatShopSolution.Admin.Services
{
    public class UserAPIClient : IUserAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent =new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/users/authenticate", httpContent);

            var token = await response.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<PagedResult<UserVm>> GetUserPagings(GetUserPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" + request.Bearer);
            var response = await client.GetAsync($"/api/users/paging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            var body = await response.Content.ReadAsStringAsync();
            var lstUsers= JsonConvert.DeserializeObject<PagedResult<UserVm>>(body);
            return lstUsers;
        }

        public async Task<bool> RegisterUser(RegisterRequest registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(registerRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/users", httpContent);
            return response.IsSuccessStatusCode;
        }

        //public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_configuration["BaseAddress"]);
        //    var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

        //    var json = JsonConvert.SerializeObject(request);
        //    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await client.PutAsync($"/api/users/{id}/roles", httpContent);
        //    var result = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

        //    return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        //}

        //public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_configuration["BaseAddress"]);
        //    var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

        //    var json = JsonConvert.SerializeObject(request);
        //    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await client.PutAsync($"/api/users/{id}", httpContent);
        //    var result = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

        //    return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        //}

    }
}
