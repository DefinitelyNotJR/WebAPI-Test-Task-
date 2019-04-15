using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIData.Entities;

namespace Parser.Services.DataService
{
    public interface IDataService
    {
         Task ClearDataAsync();
         Task SaveRssItemsAsync(IEnumerable<RssItem> rssItems);
    }
}