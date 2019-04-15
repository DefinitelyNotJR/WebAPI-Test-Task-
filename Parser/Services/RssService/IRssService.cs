using System.Threading.Tasks;
using APIData.Entities;

namespace Parser.Services.RssService
{
    public interface IRssService
    {
        Task<RssItem[]> GetRssItems();
    }
}