using AllegroApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AllegroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<User> GetTodoItems(string name)
        {
           
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("Allegro", "1"));

            var contentsUserUrl = $"https://api.github.com/users/{name}";
            var contentsUserJson = await httpClient.GetStringAsync(contentsUserUrl);
            User GetUser = JsonConvert.DeserializeObject<User>(contentsUserJson);
            var contentsRepoUrl = $"https://api.github.com/users/{name}/repos";
            var contentsRepoJson = await httpClient.GetStringAsync(contentsRepoUrl);
            
            List<Repo> GetUserRepos = JsonConvert.DeserializeObject<List<Repo>>(contentsRepoJson);

            GetUser.languageSize =
            GetUserRepos.GroupBy(
                repo => repo.Language,
                repo => repo.Size)
            .Select(
                group => new LanguageSize() { Language = group.Key, Size = group.Sum() }
            ).ToList();

            GetUser.listRepo = GetUserRepos;
            
            
            return GetUser;

        }
    }
}
