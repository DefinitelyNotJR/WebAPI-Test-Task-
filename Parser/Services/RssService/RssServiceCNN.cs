using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using APIData.Entities;
using APITypes;
using APIUtils;

namespace Parser.Services.RssService
{
    public class RssServiceCNN : IRssService
    {
        private const string UrlCNN = "http://rss.cnn.com/rss/edition.rss";
        public async Task<RssItem[]> GetRssItems()
        {
            var result = new List<RssItem>();
            var rssResult = await HttpRequestUtils.GetAsync(UrlCNN);

            var document = XDocument.Parse(rssResult.ToString());
            var xmlItems = document.Descendants("item");
            foreach (var xmlItem in xmlItems)
            {
                var rssItem = new RssItem
                {
                    Source = RssSource.CNN,
                    Title = xmlItem.Elements().First(x => x.Name == "title").Value,
                    Link = xmlItem.Elements().First(x => x.Name == "link").Value,
                    Date = Convert.ToDateTime(xmlItem.Elements().First(x => x.Name == "pubDate").Value)
                };
                result.Add(rssItem);
            }
            return result.ToArray();
        }
    }
}