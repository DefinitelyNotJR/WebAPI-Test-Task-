using System;
using System.Collections;
using System.Threading.Tasks;
using APITypes;
using APIData;
using Parser.Services.DataService;
using Parser.Services.RssService;

namespace Parser
{
    class Program
    {
        private static readonly IDataService _dataService = new DbDataService();

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {

            await _dataService.ClearDataAsync();

            try
            {
                await GetNews(RssSource.BBC);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            try
            {
                await GetNews(RssSource.CNN);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task GetNews(RssSource source)
        {

            IRssService rssService;
            string sourceName;

            switch (source)
            {
                case RssSource.Undefined:
                    throw new ArgumentOutOfRangeException();
                case RssSource.BBC:
                    rssService = new RssServiceBBC();
                    sourceName = "BBC";
                    break;
                case RssSource.CNN:
                    rssService = new RssServiceCNN();
                    sourceName = "CNN";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var items = await rssService.GetRssItems();

            Console.WriteLine($"Receiving data from {sourceName}");

            await _dataService.SaveRssItemsAsync(items);

            Console.WriteLine($"Received {items.Length} items");



            // rssService = new RssServiceBBC();

            // var rssItems = await rssService.GetRssItems();

            // Console.WriteLine($"Received {rssItems.Length} items");

            // await _dataService.SaveRssItemsAsync(rssItems);

            // Console.WriteLine("Done");
        }
    }
}

