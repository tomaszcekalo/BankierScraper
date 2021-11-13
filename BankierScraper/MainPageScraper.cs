using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankierScraper
{
    public class MainPageScraper : IMainPageScraper
    {
        //HotNews
        //DailyNews
        //HomeQuote
        //QuotationOfTheDay
        //HomeSpecialTopic
        //HomePBNews
        //Markets
        //Finances
        //Business
        //Satellites
        //

        public async Task Scrape()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://www.bankier.pl/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var cellSelector = "#hotnews-box a";
            var cells = document.QuerySelectorAll(cellSelector);
            //var titles = cells.Select(m => m);
            var articles = cells.OfType<IUrlUtilities>();
            //AngleSharp.Html.Dom.HtmlAnchorElement;
            //AngleSharp.Html.Dom.HtmlUrlBaseElement
            ScrapeHotNews(document);
            ScrapeDailyNews(document);
            Console.ReadKey();
        }

        public void ScrapeHotNews(IDocument document)
        {
            var cellSelector = "#hotnews-box a";
            var cells = document.QuerySelectorAll(cellSelector);
            var items = cells
                .Select(x => new
                {
                    x.TextContent,
                    Href = x.GetAttribute("href")
                });
        }
        public object ScrapeDailyNews(IDocument document)
        {
            var cellSelector = "#home-dailynews-box .m-title-with-label-item:not(.-orange)";
            var cells = document.QuerySelectorAll(cellSelector);
            var items = cells
                .Select(x => new
                {
                    x.QuerySelector(".m-title-with-label-item__title").TextContent,
                    Href = x.GetAttribute("href")
                });
            return items;
        }
    }
}