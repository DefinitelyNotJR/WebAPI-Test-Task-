using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using APIData.Entities;
using APIData.Repos.RssItemRepos;
using APIData.Repos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RssItemController : Controller
    {
        private readonly IRssItemRepos _rssItemRepos;

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<RssItem> Get(int id)
        {
            return await _rssItemRepos.Get(id);
        }
    }
}