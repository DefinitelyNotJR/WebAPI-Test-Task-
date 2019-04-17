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
    public class RssItemController : ControllerBase
    {
        private readonly IRssItemRepos _rssItemRepos;

        public RssItemController(IRssItemRepos rssItemRepos)
        {
            _rssItemRepos = rssItemRepos;
        }

        // GET api/rssitem
        [HttpGet]
        public async Task<RssItem[]> Get()
        {
            try
            {
                return await _rssItemRepos.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/rssitem/5
        [HttpGet("{id}")]
        public async Task<RssItem> Get(int id)
        {
            try
            {
                return await _rssItemRepos.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}